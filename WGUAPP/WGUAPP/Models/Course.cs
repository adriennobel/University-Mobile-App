using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WGUAPP.Models
{
    public class Course : INotifyPropertyChanged
    {
        private string _name = "";
        private DateTime _startDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;
        private string _status = "";
        private string _instructorName = "";
        private string _instructorPhone = "";
        private string _instructorEmail = "";
        private string _notes = "";
        private ObservableCollection<Assessment> _assessments = [];

        // Define properties
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
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

        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public string InstructorName
        {
            get { return _instructorName; }
            set
            {
                if (_instructorName != value)
                {
                    _instructorName = value;
                    OnPropertyChanged(nameof(InstructorName));
                }
            }
        }

        public string InstructorPhone
        {
            get { return _instructorPhone; }
            set
            {
                if (_instructorPhone != value)
                {
                    _instructorPhone = value;
                    OnPropertyChanged(nameof(InstructorPhone));
                }
            }
        }

        public string InstructorEmail
        {
            get { return _instructorEmail; }
            set
            {
                if (_instructorEmail != value)
                {
                    _instructorEmail = value;
                    OnPropertyChanged(nameof(InstructorEmail));
                }
            }
        }

        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged(nameof(Notes));
                }
            }
        }

        public ObservableCollection<Assessment> Assessments
        {
            get { return _assessments; }
            set
            {
                _assessments = value;
                OnPropertyChanged(nameof(Assessments));
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
