using System;
using Microsoft.Maui.Controls;
using Plugin.LocalNotification;
using WGUAPP.Models;

namespace WGUAPP.Pages;

public partial class CourseDetailPage : ContentPage
{
    private readonly Course course = new();
    private readonly Term term = new();
	public CourseDetailPage(Course course, Term term)
	{
		InitializeComponent();

		this.course = course;
        this.term = term;

		BindingContext = course;
        SetBindings();
    }

    private void SetBindings()
    {
        CourseNameLabel.SetBinding(Label.TextProperty, "Name");
        StartDateLabel.SetBinding(Label.TextProperty, new Binding("StartDate", stringFormat: "{0:MMM d, yyyy}"));
        EndDateLabel.SetBinding(Label.TextProperty, new Binding("EndDate", stringFormat: "{0:MMM d, yyyy}"));
        StatusLabel.SetBinding(Label.TextProperty, "Status");

        EditCourseNotesButton.SetBinding(Button.TextProperty, "EditNotesButtonText");
        CourseNotesLayout.SetBinding(IsVisibleProperty, "HasContentInNotes");
        CourseNotesLabel.SetBinding(Label.TextProperty, "Notes");

        PAButton.SetBinding(Button.TextProperty, "PAButtonText");
        PAGrid.SetBinding(IsVisibleProperty, "HasPA");

        OAButton.SetBinding(Button.TextProperty, "OAButtonText");
        OAGrid.SetBinding(IsVisibleProperty, "HasOA");

        InstructorNameLabel.SetBinding(Label.TextProperty, "InstructorName");
        InstructorEmailLabel.SetBinding(Label.TextProperty, "InstructorEmail");
        InstructorPhoneLabel.SetBinding(Label.TextProperty, "InstructorPhone");
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
            // Delete coresponding course
            term.RemoveCourse(course);

            // Display success message and return to main page
            await DisplayAlert("Success", "Course deleted.", "OK");
            await Navigation.PopAsync();
        }
    }
}