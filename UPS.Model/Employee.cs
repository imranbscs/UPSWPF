using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace UPS.Model
{
    public class Employee : ViewModelBase 
    {
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        
        private string _name;
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("name");
            }
        }

        private string _email;
        public string email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged("email");
            }
        }

        private string _gender;
        public string gender
        {
            get
            {
                return _gender;
            }
            set
            {

                _gender = value;
                OnPropertyChanged("gender");

            }
        }

        private string _status;
        public string status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged("status");
            }
        }

        private ObservableCollection<Employee> _employee;
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
                OnPropertyChanged("Employees");
            }
        }

        private void Employee_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Employees");
        }


        private string _errorname = "";
        public string errorname
        {
            get
            {
                return _errorname;
            }
            set
            {
                _errorname = value;
                OnPropertyChanged("errorname");
            }
        }

        private string _erroremail = "";
        public string erroremail
        {
            get
            {
                return _erroremail;
            }
            set
            {
                _erroremail = value;
                OnPropertyChanged("erroremail");
            }
        }

    }

}

