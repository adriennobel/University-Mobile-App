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

            DegreePlan.AddTerm(new Term
            {
                Title = "Spring term",
                Courses =
                [
                    new Course
                    {
                        Name = "Psychology 101",
                        Status = "Plan to take",
                        InstructorName = "John Doe",
                        InstructorPhone = "555-555-5555",
                        InstructorEmail = "john.doe@email.com"
                    }
                ]
            });

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

        private async void CourseGrid_Tapped(object sender, TappedEventArgs e)
        {
            Grid grid = (Grid)sender;
            Course course = (Course)grid.BindingContext;

            ListView listView = (ListView)grid.Parent.Parent;
            Term term = (Term)listView.BindingContext;

            await Navigation.PushAsync(new CourseDetailPage(course, term));
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