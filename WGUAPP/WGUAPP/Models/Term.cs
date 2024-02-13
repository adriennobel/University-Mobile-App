using Microsoft.Maui.Controls.Compatibility;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WGUAPP.Models
{
    public class Term : INotifyPropertyChanged
    {
        private string _title = "";
        private DateTime _startDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;
        private ObservableCollection<Course> _courses = [];

        // Define properties
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        public ObservableCollection<Course> Courses
        {
            get { return _courses; }
            set
            {
                _courses = value;
                OnPropertyChanged(nameof(Courses));
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }

        public void RemoveCourse(Course course)
        {
            Courses.Remove(course);
        }
    }
}
