using SQLite;

namespace WGUAPP.Models
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;

        public void AddCourse(Course course)
        {
            //Courses.Add(course);
        }

        public void RemoveCourse(Course course)
        {
            //Courses.Remove(course);
        }
    }
}
