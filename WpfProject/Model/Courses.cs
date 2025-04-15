using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfProject.Model
{
    public class Courses : ObservableCollection<Course>
    {
        public Courses()
        {
            this.Add(new Course
            {
                Name = "Learning WPF",
                Id = 1001,
                StartDate = new DateTime(2010, 1, 11),
                EndDate = new DateTime(2010, 1, 22)
            });
            this.Add(new Course
            {
                Name = "Learning Silverlight",
                Id = 1002,
                StartDate = new DateTime(2010, 1, 25),
                EndDate = new DateTime(2010, 2, 5)
            });
            this.Add(new Course
            {
                Name = "Learning Expression Blend",
                Id = 1003,
                StartDate = new DateTime(2010, 2, 8),
                EndDate = new DateTime(2010, 2, 19)
            });
            this.Add(new Course
            {
                Name = "Learning LINQ",
                Id = 1004,
                StartDate = new DateTime(2010, 2, 22),
                EndDate = new DateTime(2010, 3, 5)
            });
        }
    }

    public class Course : IEditableObject, INotifyPropertyChanged, IDataErrorInfo
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private int _number;
        public int Id
        {
            get
            {
                return _number;
            }
            set
            {
                if (_number == value) return;
                _number = value;
                OnPropertyChanged("Id");
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (_startDate == value) return;
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (_endDate == value) return;
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        #region IEditableObject

        private Course backupCopy;
        private bool inEdit;

        public void BeginEdit()
        {
            if (inEdit) return;
            inEdit = true;
            backupCopy = this.MemberwiseClone() as Course;
        }

        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            this.Name = backupCopy.Name;
            this.Id = backupCopy.Id;
            this.StartDate = backupCopy.StartDate;
            this.EndDate = backupCopy.EndDate;
        }

        public void EndEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            backupCopy = null;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IDataErrorInfo
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                        {
                            return "Name is required.";
                        }
                        break;
                    case nameof(StartDate):
                    case nameof(EndDate):
                        if (StartDate > EndDate)
                        {
                            return "Start Date must be earlier than End Date.";
                        }
                        break;
                }
                return null;
            }
        }
        #endregion

    }
}
