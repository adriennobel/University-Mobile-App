using WGUAPP.Models;

namespace WGUAPP.Pages;

public partial class CourseAssessmentPage : ContentPage
{
    private readonly Course course = new();
    private readonly string assessmentType = "";
    public CourseAssessmentPage(Course course, string assessmentType)
	{
		InitializeComponent();

        this.course = course;
        this.assessmentType = assessmentType;

        Title = assessmentType + Title;
        BindingContext = course;

        if (assessmentType == "Performance")
        {
            NameEntry.Text = course.PerformanceAssessment.Name;
            StartDatePicker.Date = course.PerformanceAssessment.StartDate;
            EndDatePicker.Date = course.PerformanceAssessment.EndDate;

            DeleteButton.SetBinding(IsVisibleProperty, "HasPA");
        }
        else if (assessmentType == "Objective")
        {
            NameEntry.Text = course.ObjectiveAssessment.Name;
            StartDatePicker.Date = course.ObjectiveAssessment.StartDate;
            EndDatePicker.Date = course.ObjectiveAssessment.EndDate;

            DeleteButton.SetBinding(IsVisibleProperty, "HasOA");
        }
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
            if (assessmentType == "Performance")
            {
                if (name.Trim() == course.PerformanceAssessment.Name && startDate == course.PerformanceAssessment.StartDate && endDate == course.PerformanceAssessment.EndDate)
                {
                    await DisplayAlert("Alert", "No change detected.", "OK");
                    return;
                }

                // Register notifications for start and end dates
                int startDateAlertID;
                int endDateAlertID;
                if (course.PerformanceAssessment.StartDateAlertID == -1 && course.PerformanceAssessment.EndDateAlertID == -1)
                {
                    startDateAlertID = NotificationService.RegisterNotification(course.Name, "Performance Assessment started", startDate);
                    endDateAlertID = NotificationService.RegisterNotification(course.Name, "Performance Assessment ended", endDate);
                    course.PerformanceAssessment.StartDateAlertID = startDateAlertID;
                    course.PerformanceAssessment.EndDateAlertID = endDateAlertID;
                }
                else
                {
                    NotificationService.UpdateRegisteredNotification(course.PerformanceAssessment.StartDateAlertID, course.Name, "Performance Assessment started", startDate);
                    NotificationService.UpdateRegisteredNotification(course.PerformanceAssessment.StartDateAlertID, course.Name, "Performance Assessment ended", endDate);
                }

                // Update course instance with new values
                course.PerformanceAssessment.Name = name;
                course.PerformanceAssessment.StartDate = startDate;
                course.PerformanceAssessment.EndDate = endDate;
                course.HasPA = true;
                course.PAButtonText = "Edit";
            }
            else if (assessmentType == "Objective")
            {
                if (name.Trim() == course.ObjectiveAssessment.Name && startDate == course.ObjectiveAssessment.StartDate && endDate == course.ObjectiveAssessment.EndDate)
                {
                    await DisplayAlert("Alert", "No change detected.", "OK");
                    return;
                }

                // Register notifications for start and end dates
                int startDateAlertID;
                int endDateAlertID;
                if (course.ObjectiveAssessment.StartDateAlertID == -1 && course.ObjectiveAssessment.EndDateAlertID == -1)
                {
                    startDateAlertID = NotificationService.RegisterNotification(course.Name, "Objective Assessment started", startDate);
                    endDateAlertID = NotificationService.RegisterNotification(course.Name, "Objective Assessment ended", endDate);
                    course.ObjectiveAssessment.StartDateAlertID = startDateAlertID;
                    course.ObjectiveAssessment.EndDateAlertID = endDateAlertID;
                }
                else
                {
                    NotificationService.UpdateRegisteredNotification(course.ObjectiveAssessment.StartDateAlertID, course.Name, "Objective Assessment started", startDate);
                    NotificationService.UpdateRegisteredNotification(course.ObjectiveAssessment.StartDateAlertID, course.Name, "Objective Assessment ended", endDate);
                }

                // Update course instance with new values
                course.ObjectiveAssessment.Name = name;
                course.ObjectiveAssessment.StartDate = startDate;
                course.ObjectiveAssessment.EndDate = endDate;
                course.HasOA = true;
                course.OAButtonText = "Edit";
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
            if (assessmentType == "Performance")
            {
                NotificationService.ClearAndCancelNotification(course.PerformanceAssessment.StartDateAlertID);
                NotificationService.ClearAndCancelNotification(course.PerformanceAssessment.EndDateAlertID);

                // Update course instance with reset values
                course.PerformanceAssessment.Name = "";
                course.PerformanceAssessment.StartDate = DateTime.Now;
                course.PerformanceAssessment.EndDate = DateTime.Now;
                course.HasPA = false;
                course.PAButtonText = "Add";
            }
            else if (assessmentType == "Objective")
            {
                NotificationService.ClearAndCancelNotification(course.ObjectiveAssessment.StartDateAlertID);
                NotificationService.ClearAndCancelNotification(course.ObjectiveAssessment.EndDateAlertID);

                // Update course instance with reset values
                course.ObjectiveAssessment.Name = "";
                course.ObjectiveAssessment.StartDate = DateTime.Now;
                course.ObjectiveAssessment.EndDate = DateTime.Now;
                course.HasOA = false;
                course.OAButtonText = "Add";
            }

            // Display success message and return to main page
            await DisplayAlert("Success", "Assessment deleted.", "OK");
            await Navigation.PopAsync();
        }
    }
}