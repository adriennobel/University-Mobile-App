using WGUAPP.Models;

namespace WGUAPP.Pages;

public partial class CourseAssessmentPage : ContentPage
{
    private Assessment assessment = new();
    private readonly string assessmentType = "";
    private readonly Course course;

    public CourseAssessmentPage(Course course, string assessmentType)
	{
		InitializeComponent();

        this.assessmentType = assessmentType;
        this.course = course;

        Title = assessmentType + Title;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        assessment = await DatabaseService.GetAssessment(course.Id, assessmentType);

        NameEntry.Text = assessment.Name;
        StartDatePicker.Date = assessment.StartDate;
        EndDatePicker.Date = assessment.EndDate;
    }

    private async void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            // Retrieve data from controls
            string name = NameEntry.Text;
            DateTime startDate = StartDatePicker.Date;
            DateTime endDate = EndDatePicker.Date;

            // Perform validation
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Validation Error", "Name cannot be empty.", "OK");
                return;
            }

            // Check for any change
            if (name.Trim() == assessment.Name && startDate == assessment.StartDate && endDate == assessment.EndDate)
            {
                await DisplayAlert("Alert", "No change detected.", "OK");
                return;
            }

            // Register or update notifications for start and end dates
            if (assessment.Name == "")
            {
                int startDateAlertID = NotificationService.RegisterNotification(name, $"{assessmentType} Assessment started", startDate);
                int endDateAlertID = NotificationService.RegisterNotification(name, $"{assessmentType} Assessment ended", endDate);

                // Create assessment object
                Assessment newAssessment = new()
                {
                    CourseId = course.Id,
                    Name = name,
                    Type = assessmentType,
                    StartDate = startDate,
                    EndDate = endDate,
                    StartDateAlertID = startDateAlertID,
                    EndDateAlertID = endDateAlertID,
                };

                await DatabaseService.AddAssessment(newAssessment);
                await DatabaseService.UpdateCourseHasAssessment(course.Id, assessmentType == "Performance" || course.HasPA , assessmentType == "Objective" || course.HasOA);
            }
            else
            {
                NotificationService.UpdateRegisteredNotification(assessment.StartDateAlertID, name, $"{assessmentType} Assessment started", startDate);
                NotificationService.UpdateRegisteredNotification(assessment.StartDateAlertID, name, $"{assessmentType} Assessment ended", endDate);

                // Create assessment object
                Assessment newAssessment = new()
                {
                    Name = name,
                    StartDate = startDate,
                    EndDate = endDate,
                };

                // Update assessment instance with new values
                await DatabaseService.UpdateAssessment(assessment.Id, newAssessment);
                await DatabaseService.UpdateCourseHasAssessment(course.Id, assessmentType == "Performance" || course.HasPA, assessmentType == "Objective" || course.HasOA);
            }

            // Display success message and pop the modal page
            await DisplayAlert("Success", "Assessment updated sucessfully", "OK");
            await Navigation.PopAsync();
        }
        catch
        {
            await DisplayAlert("Error", "An error occured while adding this new assessment.", "OK");
        }
    }

    private async void CancelButton_Click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void DeleteButton_Click(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmation", "Are you sure you want to delete this assessment?", "Yes", "No");

        if (answer)
        {
            // Delete assessment
            await DatabaseService.RemoveAssessment(assessment.Id);
            await DatabaseService.UpdateCourseHasAssessment(course.Id, assessmentType != "Performance" && course.HasPA, assessmentType != "Objective" && course.HasOA);

            // Display success message and return to main page
            await DisplayAlert("Success", "Assessment deleted.", "OK");
            await Navigation.PopAsync();
        }
    }
}