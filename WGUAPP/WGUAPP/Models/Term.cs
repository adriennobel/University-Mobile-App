using Microsoft.Maui.Controls.Compatibility;
using System.Collections.ObjectModel;

namespace WGUAPP.Models
{
    public class Term
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ObservableCollection<Course> Courses { get; set; } = [];

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }
    }
}
