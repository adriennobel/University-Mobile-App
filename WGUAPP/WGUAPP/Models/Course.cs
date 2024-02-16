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
        private Assessment _performanceAssessment = new();
        private Assessment _objectiveAssessment = new();

        private bool _hasContentInNotes = false;
        private string _editNotesButtonText = "Add";

        private bool _hasPA = false;
        private string _paButtonText = "Add";

        private bool _hasOA = false;
        private string _oaButtonText = "Add";

        private int _startDateAlertID = -1;
        private int _endDateAlertID = -1;

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

        public Assessment PerformanceAssessment
        {
            get { return _performanceAssessment; }
            set
            {
                _performanceAssessment = value;
                OnPropertyChanged(nameof(PerformanceAssessment));
            }
        }

        public Assessment ObjectiveAssessment
        {
            get { return _objectiveAssessment; }
            set
            {
                _objectiveAssessment = value;
                OnPropertyChanged(nameof(ObjectiveAssessment));
            }
        }

        public bool HasContentInNotes
        {
            get { return _hasContentInNotes; }
            set
            {
                if (_hasContentInNotes != value)
                {
                    _hasContentInNotes = value;
                    OnPropertyChanged(nameof(HasContentInNotes));
                }
            }
        }

        public string EditNotesButtonText
        {
            get { return _editNotesButtonText; }
            set
            {
                if (_editNotesButtonText != value)
                {
                    _editNotesButtonText = value;
                    OnPropertyChanged(nameof(EditNotesButtonText));
                }
            }
        }

        public bool HasPA
        {
            get { return _hasPA; }
            set
            {
                if (_hasPA != value)
                {
                    _hasPA = value;
                    OnPropertyChanged(nameof(HasPA));
                }
            }
        }

        public string PAButtonText
        {
            get { return _paButtonText; }
            set
            {
                if (_paButtonText != value)
                {
                    _paButtonText = value;
                    OnPropertyChanged(nameof(PAButtonText));
                }
            }
        }

        public bool HasOA
        {
            get { return _hasOA; }
            set
            {
                if (_hasOA != value)
                {
                    _hasOA = value;
                    OnPropertyChanged(nameof(HasOA));
                }
            }
        }

        public string OAButtonText
        {
            get { return _oaButtonText; }
            set
            {
                if (_oaButtonText != value)
                {
                    _oaButtonText = value;
                    OnPropertyChanged(nameof(OAButtonText));
                }
            }
        }

        public int StartDateAlertID
        {
            get { return _startDateAlertID; }
            set
            {
                if (_startDateAlertID != value)
                {
                    _startDateAlertID = value;
                    OnPropertyChanged(nameof(StartDateAlertID));
                }
            }
        }

        public int EndDateAlertID
        {
            get { return _endDateAlertID; }
            set
            {
                if (_endDateAlertID != value)
                {
                    _endDateAlertID = value;
                    OnPropertyChanged(nameof(EndDateAlertID));
                }
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
