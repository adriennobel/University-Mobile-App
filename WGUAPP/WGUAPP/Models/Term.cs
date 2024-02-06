using System.Collections.ObjectModel;

namespace WGUAPP.Models
{
    public class Term
    {
        public string Title { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ObservableCollection<Course> Courses { get; set; } = [];
    }

}
