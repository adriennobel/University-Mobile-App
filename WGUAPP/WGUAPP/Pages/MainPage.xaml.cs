using WGUAPP.Models;

namespace WGUAPP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            DegreePlan.AddTerm(new Term { Title = "Term 1"});
            DegreePlan.AddTerm(new Term { Title = "Term 2"});
            DegreePlan.AddTerm(new Term { Title = "Spring term" });
            TermsListView.ItemsSource = DegreePlan.Terms;
        }

        private void TermChevronBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            VerticalStackLayout parent = (VerticalStackLayout)button.Parent.Parent;

            if (parent.Children[1] is VerticalStackLayout child)
            {
                child.IsVisible = !child.IsVisible;
            }
        }

        private async void AddTermBtn_Click(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddTermPage());
        }

    }
}