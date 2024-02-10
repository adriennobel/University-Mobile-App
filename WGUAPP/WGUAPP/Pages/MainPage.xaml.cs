using System.Collections.ObjectModel;
using System.Windows.Input;
using WGUAPP.Models;

namespace WGUAPP.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            DegreePlan.AddTerm(new Term { Title = "Term 1"});
            DegreePlan.AddTerm(new Term { Title = "Term 2"});
            DegreePlan.AddTerm(new Term
            {
                Id = 0,
                Title = "Spring term",
                Courses =
                [
                    new Course { Name = "English 101", Status = "Plan to take" },
                    new Course { Name = "Psychology 101", Status = "Dropped" }
                ]
            });

            TermsListView.ItemsSource = DegreePlan.Terms;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Update the ItemsSource of the TermsListView
            TermsListView.ItemsSource = null;
            TermsListView.ItemsSource = DegreePlan.Terms;
        }

        private void TermChevronButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            VerticalStackLayout parent = (VerticalStackLayout)button.Parent.Parent;

            if (parent.Children[1] is VerticalStackLayout child)
            {
                child.IsVisible = !child.IsVisible;
            }
        }

        private async void AddCourseButton_Click(Object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Term term = (Term)button.CommandParameter;

            await Navigation.PushAsync(new AddCoursePage(term));
        }

        private async void EditTermButton_Click(Object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Term term = (Term)button.CommandParameter;

            await Navigation.PushAsync(new EditTermPage(term));
        }

        private async void AddTermButton_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTermPage());
        }

    }
}