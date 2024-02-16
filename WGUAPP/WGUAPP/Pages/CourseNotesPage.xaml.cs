using WGUAPP.Models;

namespace WGUAPP.Pages;

public partial class CourseNotesPage : ContentPage
{
    private readonly Course course = new();

    public CourseNotesPage(Course course)
	{
		InitializeComponent();

        this.course = course;

        CourseNotesEditor.Text = course.Notes;
    }

    private async void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            // Retrieve data from controls
            string courseNotes = CourseNotesEditor.Text;

            // Check for any change
            if (courseNotes == course.Notes)
            {
                await DisplayAlert("Alert", "No change detected.", "OK");
                return;
            }

            // Perform validation
            if (string.IsNullOrEmpty(courseNotes))
            {
                bool answer = await DisplayAlert("Warning", "Are you sure you want to save an empty note?", "Yes", "No");

                if (answer)
                {
                    // Update course instance with new values
                    course.Notes = "";
                    course.HasContentInNotes = false;
                    course.EditNotesButtonText = "Add";
                }
                else
                {
                    return;
                }
            }
            else
            {
                // Update course instance with new values
                course.Notes = courseNotes;
                course.HasContentInNotes = true;
                course.EditNotesButtonText = "Edit";
            }

            // Display success message and pop the modal page
            await DisplayAlert("Success", "Course updated sucessfully", "OK");
            await Navigation.PopAsync();
        }
        catch
        {
            await DisplayAlert("Error", "An error occured while adding this note.", "OK");
        }
    }


    private async void CancelButton_Click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}