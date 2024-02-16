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

        if (assessmentType == "Performance")
        {
            NameEntry.Text = course.PerformanceAssessment.Name;
            StartDatePicker.Date = course.PerformanceAssessment.StartDate;
            EndDatePicker.Date = course.PerformanceAssessment.EndDate;
        }
        else if (assessmentType == "Objective")
        {
            NameEntry.Text = course.ObjectiveAssessment.Name;
            StartDatePicker.Date = course.ObjectiveAssessment.StartDate;
            EndDatePicker.Date = course.ObjectiveAssessment.EndDate;
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
}