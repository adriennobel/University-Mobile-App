
namespace WGUAPP.Models
{
    public class Assessment
    {
        public string AssessmentName { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool NotificationsEnabled { get; set; }
    }

}
