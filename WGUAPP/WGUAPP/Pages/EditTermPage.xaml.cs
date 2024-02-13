using WGUAPP.Models;

namespace WGUAPP.Pages;

public partial class EditTermPage : ContentPage
{
    readonly Term term = new();

    public EditTermPage(Term term)
	{
		InitializeComponent();

        this.term = term;
		TermNameEntry.Text = term.Title;
		StartDatePicker.Date = term.StartDate;
		EndDatePicker.Date = term.EndDate;
	}

    private async void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            // Retrieve data from controls
            string title = TermNameEntry.Text;
            DateTime startDate = StartDatePicker.Date;
            DateTime endDate = EndDatePicker.Date;

            // Check for any change
            if (title.Trim() == term.Title && startDate == term.StartDate && endDate == term.EndDate)
            {
                await DisplayAlert("Alert", "No change detected.", "OK");
                return;
            }

            // Perform validation
            if (string.IsNullOrEmpty(title))
            {
                await DisplayAlert("Validation Error", "Title cannot be empty.", "OK");
                return;
            }

            // Update term instance with new values
            term.Title = title.Trim();
            term.StartDate = startDate;
            term.EndDate = endDate;

            // Display success message and return to main page
            await DisplayAlert("Success", "Term updated sucessfully.", "OK");
            await Navigation.PopAsync();
        }
        catch
        {
            await DisplayAlert("Error", "An error occured while adding this new term.", "OK");
        }
    }

    private async void CancelButton_Click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void DeleteButton_Click(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmation", "Are you sure you want to delete this term?", "Yes", "No");

        if (answer)
        {
            // Delete coresponding term
            DegreePlan.RemoveTerm(term);

            // Display success message and return to main page
            await DisplayAlert("Success", "Term deleted.", "OK");
            await Navigation.PopAsync();
        }
    }
}