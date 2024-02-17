using System;
using System.ComponentModel;

namespace WGUAPP.Models
{
    public class Assessment : INotifyPropertyChanged
    {
        private string _name = "";
        private DateTime _startDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;

        private int _startDateAlertID = -1;
        private int _endDateAlertID = -1;

        public string Name
        {
            get => _name;
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
            get => _startDate;
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
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
