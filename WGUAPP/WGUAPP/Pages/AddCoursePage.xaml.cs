using WGUAPP.Models;

namespace WGUAPP.Pages;

public partial class AddCoursePage : ContentPage
{
    readonly Term term = new();
    readonly List<string> statusList = ["In progress", "Completed", "Dropped", "Plan to take"];

	public AddCoursePage(Term term)
	{
		InitializeComponent();

        this.term = term;
        StatusPicker.ItemsSource = statusList;
	}

    private async void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            // Retrieve data from controls
            string name = CourseNameEntry.Text;
            DateTime startDate = StartDatePicker.Date;
            DateTime endDate = EndDatePicker.Date;
            string status = (string)StatusPicker.SelectedItem;

            // Perform validation
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Validation Error", "Name cannot be empty", "OK");
                return;
            }

            // Create a new course object
            Course course = new()
            {
                Name = name.Trim(),
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
            };

            //Add the course object to Term object
            term.AddCourse(course);

            // Display success message and pop the modal page
            await DisplayAlert("Success", "New course added", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occured while adding this new term.\n\n{ex.Message}", "OK");
        }
    }

    private async void CancelButton_Click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}