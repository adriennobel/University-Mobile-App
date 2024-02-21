using Plugin.LocalNotification;
using WGUAPP.Models;

namespace WGUAPP.Pages;

public partial class CourseDetailPage : ContentPage
{
    private Course course = new();
    private Assessment PA = new();
    private Assessment OA = new();
	public CourseDetailPage(Course course)
	{
		InitializeComponent();

		this.course = course;

        SetValues();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        course = await DatabaseService.RefreshCourse(course.Id);
        PA = await DatabaseService.GetAssessment(course.Id, "Performance");
        OA = await DatabaseService.GetAssessment(course.Id, "Objective");
        SetValues();
    }

    private void SetValues()
    {
        CourseNameLabel.Text = course.Name;
        StartDateLabel.Text = course.StartDate.ToString("MMM d, yyyy");
        EndDateLabel.Text = course.EndDate.ToString("MMM d, yyyy");
        StatusLabel.Text = course.Status;

        EditCourseNotesButton.Text = string.IsNullOrEmpty(course.Notes) ? "Add" : "Edit";
        CourseNotesLayout.IsVisible = !string.IsNullOrEmpty(course.Notes);
        CourseNotesLabel.Text = course.Notes;

        PAButton.Text = course.HasPA ? "Edit" : "Add";
        PAGrid.IsVisible = course.HasPA;
        PANameLabel.Text = PA.Name;
        PADateLabel.Text = PA.EndDate.ToString("MMM d, yyyy");

        OAButton.Text = course.HasOA ? "Edit" : "Add";
        OAGrid.IsVisible = course.HasOA;
        OANameLabel.Text = OA.Name;
        OADateLabel.Text = OA.EndDate.ToString("MMM d, yyyy");

        InstructorNameLabel.Text = course.InstructorName;
        InstructorEmailLabel.Text = course.InstructorEmail;
        InstructorPhoneLabel.Text = course.InstructorPhone;
    }

    private async void EditCourseNotesButton_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CourseNotesPage(course));
    }

    private async void ShareCourseNotesButton_Click(object sender, EventArgs e)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = course.Notes,
            Title = course.Name + "notes"
        });
    }

    private async void PAButton_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CourseAssessmentPage(course, "Performance"));
    }

    private async void OAButton_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CourseAssessmentPage(course, "Objective"));
    }

    private async void EditCourseButton_Click(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new EditCoursePage(course));
	}

    private async void DeleteCourseButton_Click(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmation", "Are you sure you want to delete this course?", "Yes", "No");

        if (answer)
        {
            NotificationService.ClearAndCancelNotification(course.StartDateAlertID);
            NotificationService.ClearAndCancelNotification(course.EndDateAlertID);

            // Delete coresponding course
            await DatabaseService.RemoveCourse(course.Id);

            // Display success message and return to main page
            await DisplayAlert("Success", "Course deleted.", "OK");
            await Navigation.PopAsync();
        }
    }
}