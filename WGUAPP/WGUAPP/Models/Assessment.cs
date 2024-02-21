using System;
using SQLite;

namespace WGUAPP.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; } = "";
        public string Type { get; set; } = ""; // Performance or Objective
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;

        public int StartDateAlertID { get; set; } = -1;
        public int EndDateAlertID { get; set; } = -1;
    }
}
