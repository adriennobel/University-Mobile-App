using WGUAPP.Models;

namespace WGUAPP;

public partial class AddTermPage : ContentPage
{
	public AddTermPage()
	{
		InitializeComponent();
	}

	private async void SaveButton_Click(object sender, EventArgs e)
	{
		try
		{
            // Retrieve data from controls
            string title = TermNameEntry.Text;
            DateTime startDate = StartDatePicker.Date;
            DateTime endDate = EndDatePicker.Date;

            // Perform validation
            if (string.IsNullOrEmpty(title))
            {
                await DisplayAlert("Validation Error", "Title cannot be empty", "OK");
                return;
            }

            // Create a new term
            Term newTerm = new()
            {
                Title = title.Trim(),
                StartDate = startDate,
                EndDate = endDate
            };

            // Add the new term to the DegreePlan
            DegreePlan.AddTerm(newTerm);

            // Display success message and pop the modal page
            await DisplayAlert("Success", "New term added", "OK");
            await Navigation.PopModalAsync();
        }
		catch (Exception ex)
		{
            await DisplayAlert("Error", $"An error occured while adding this new term.\n\n{ex.Message}", "OK");
        }
	}
	
    private async void CancelButton_Click(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}
}