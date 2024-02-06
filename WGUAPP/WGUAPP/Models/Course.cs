using System.Collections.ObjectModel;

namespace WGUAPP.Models
{
    public class Course
    {
        public string Name { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = ""; // e.g., in progress, completed, dropped, plan to take
        public string InstructorName { get; set; } = "";
        public string InstructorPhone { get; set; } = "";
        public string InstructorEmail { get; set; } = "";
        public string Notes { get; set; } = "";
        public ObservableCollection<Assessment> Assessments { get; set; } = [];
    }

}
