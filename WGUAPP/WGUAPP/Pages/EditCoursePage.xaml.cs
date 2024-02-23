using System.Text.RegularExpressions;
using WGUAPP.Models;

namespace WGUAPP.Pages;

public partial class EditCoursePage : ContentPage
{
    readonly Course course = new();
    readonly List<string> statusList = ["In progress", "Completed", "Dropped", "Plan to take"];

    public EditCoursePage(Course course)
	{
		InitializeComponent();

        this.course = course;
        StatusPicker.ItemsSource = statusList;

        CourseNameEntry.Text = course.Name;
        StartDatePicker.Date = course.StartDate;
        EndDatePicker.Date = course.EndDate;
        StatusPicker.SelectedItem = course.Status;
        InstructorNameEntry.Text = course.InstructorName;
        InstructorPhoneEntry.Text = course.InstructorPhone;
        InstructorEmailEntry.Text = course.InstructorEmail;
        NotificationsCheckbox.IsChecked = course.NotificationsEnabled;
    }

    private void InstructorPhoneEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry entry = (Entry)sender;
        string text = entry.Text;

        // Remove non-numeric characters from the input
        string numericText = new(text.Where(c => char.IsDigit(c)).ToArray());

        // Format the phone number with your desired format
        if (numericText.Length >= 4)
        {
            numericText = $"({numericText[..3]}" +
                          $"{(numericText.Length > 3 ? ") " : "")}{numericText[3..]}";
        }
        if (numericText.Length >= 9)
        {
            numericText = $"{numericText[..9]}" +
                          $"{(numericText.Length > 9 ? "-" : "")}{numericText[9..]}";
        }

        // Limit the length to 14 characters
        if (numericText.Length > 14)
        {
            numericText = numericText[..14];
        }

        // Update the entry text with the formatted phone number
        entry.Text = numericText;
        // Set the cursor position to the end of the text
        entry.CursorPosition = numericText.Length;
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
            string instructorName = InstructorNameEntry.Text;
            string instructorPhone = InstructorPhoneEntry.Text;
            string instructorEmail = InstructorEmailEntry.Text;

            // Check for any change
            if (name.Trim() == course.Name && startDate == course.StartDate && endDate == course.EndDate && status == course.Status &&
                instructorName.Trim() == course.InstructorName && instructorPhone == course.InstructorPhone && instructorEmail.Trim() == course.InstructorEmail &&
                NotificationsCheckbox.IsChecked == course.NotificationsEnabled)
            {
                await DisplayAlert("Alert", "No change detected.", "OK");
                return;
            }

            // Perform validation
            if (IsFieldEmpty(name, "Course name") || IsFieldEmpty(instructorName, "Instructor name") ||
                IsFieldEmpty(instructorPhone, "Instructor phone") || IsFieldEmpty(instructorEmail, "Instructor email"))
            {
                return;
            }

            if (!IsPhoneNumberValid(instructorPhone.Trim()))
            {
                await DisplayAlert("Validation Error", "Please enter a valid phone number", "OK");
                return;
            }

            if (!IsEmailValid(instructorEmail.Trim()))
            {
                await DisplayAlert("Validation Error", "Please enter a valid email address", "OK");
                return;
            }

            // Create a new course object 
            Course newCourse = new()
            {
                Name = name.Trim(),
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                InstructorName = instructorName.Trim(),
                InstructorPhone = instructorPhone,
                InstructorEmail = instructorEmail.Trim()
            };

            // Update or register notifications based on checkbox
            if (course.NotificationsEnabled && NotificationsCheckbox.IsChecked)
            {
                NotificationService.UpdateRegisteredNotification(course.StartDateAlertID, name.Trim(), "Course started", startDate);
                NotificationService.UpdateRegisteredNotification(course.EndDateAlertID, name.Trim(), "Course ended", endDate);
            }
            else if (course.NotificationsEnabled && !NotificationsCheckbox.IsChecked)
            {
                NotificationService.ClearAndCancelNotification(course.StartDateAlertID);
                NotificationService.ClearAndCancelNotification(course.EndDateAlertID);
            }
            else if (!course.NotificationsEnabled && NotificationsCheckbox.IsChecked)
            {
                int startDateAlertID = NotificationService.RegisterNotification(name.Trim(), "Course started", startDate);
                int endDateAlertID = NotificationService.RegisterNotification(name.Trim(), "Course ended", endDate);

                newCourse.NotificationsEnabled = true;
                newCourse.StartDateAlertID = startDateAlertID;
                newCourse.EndDateAlertID = endDateAlertID;
            }

            // call update course service
            await DatabaseService.UpdateCourse(course.Id, newCourse);

            // Display success message and pop the modal page
            await DisplayAlert("Success", "Course updated sucessfully", "OK");
            await Navigation.PopAsync();
        }
        catch
        {
            await DisplayAlert("Error", "An error occured while updating this course.", "OK");
        }
    }

    private async void CancelButton_Click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private bool IsFieldEmpty(string fieldValue, string fieldName)
    {
        if (string.IsNullOrEmpty(fieldValue))
        {
            DisplayAlert("Validation Error", $"{fieldName} cannot be empty", "OK");
            return true;
        }
        else return false;
    }

    private bool IsPhoneNumberValid(string phoneNumber)
    {
        // Regular expression for phone number validation
        string phonePattern = @"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$";
        return Regex.IsMatch(phoneNumber, phonePattern);
    }

    private bool IsEmailValid(string email)
    {
        // Regular expression for email validation
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, emailPattern);
    }

}