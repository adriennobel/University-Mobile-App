using SQLite;

namespace WGUAPP.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TermId { get; set; }
        public string Name { get; set; } = "";
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = ""; // "Plan to take", "In progress", "Completed", "Dropped"
        public string InstructorName { get; set; } = "";
        public string InstructorPhone { get; set; } = "";
        public string InstructorEmail { get; set; } = "";
        public string Notes { get; set; } = "";

        public bool HasPA { get; set; } = false;
        public bool HasOA { get; set; } = false;

        public int StartDateAlertID { get; set; } = -1;
        public int EndDateAlertID { get; set; } = -1;
    }
}
