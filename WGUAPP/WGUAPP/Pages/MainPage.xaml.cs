using SQLite;
using WGUAPP.Models;

namespace WGUAPP.Pages
{
    public partial class MainPage : ContentPage
    {
        private bool isEvaluationDataCreated = false;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Check if evaluation data creation has already been performed
            if (!isEvaluationDataCreated)
            {
                // Call the asynchronous initialization method
                await DatabaseService.CreateEvaluationData();

                // Set the flag to true to indicate that data creation has been completed
                isEvaluationDataCreated = true;
            }

            TermsListView.ItemsSource = await DatabaseService.GetTerms();
        }

        private async void TermChevronButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Term term = (Term)button.CommandParameter;
            VerticalStackLayout termLayout = (VerticalStackLayout)button.Parent.Parent;

            if (termLayout.Children[1] is VerticalStackLayout coursesLayout)
            {
                coursesLayout.IsVisible = !coursesLayout.IsVisible;

                // load courses when the courses layout is made visible
                if (coursesLayout.IsVisible == true)
                {
                    VerticalStackLayout coursesLayout2 = (VerticalStackLayout)coursesLayout.Children[1];
                    ListView coursesListView = (ListView)coursesLayout2.Children[0];

                    coursesListView.ItemsSource = await DatabaseService.GetCourses(term.Id);
                }
            }
        }

        private async void CourseGrid_Tapped(object sender, TappedEventArgs e)
        {
            Grid grid = (Grid)sender;
            Course course = (Course)grid.BindingContext;

            await Navigation.PushAsync(new CourseDetailPage(course));
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