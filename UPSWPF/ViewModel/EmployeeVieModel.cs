using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using UPS.Repository;
using UPS.Model;
using System.Windows.Controls;
using System.Net.Mail;
using System.Text.RegularExpressions;
using UPSWPF;

namespace UPSWPPF.ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        private ICommand _saveCommand;
        private ICommand _resetCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private ICommand _searchCommand;
        private EmployeeRepository _repository;
        public Employee Employee { get; set; }
        

        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                    _resetCommand = new RelayCommand(param => ResetData(), null);

                return _resetCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(param => SaveData(this.Employee), null);

                return _saveCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new RelayCommand(param => EditData((int)param), null);

                return _editCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(param => DeleteEmployee((Employee)param), null);
                
                return _deleteCommand;
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                    _searchCommand = new RelayCommand(param => SearchEmployee(this.Employee.name), null);

                return _searchCommand;
            }
        }

       
        public EmployeeViewModel(int page )
        {
           
            _repository = new EmployeeRepository();
            Employee = new Employee();
            GetAll(page);
        }

        public void ResetData()
        {
            Employee.name = string.Empty;
            Employee.Id = 0;
            Employee.email = string.Empty;
            Employee.status = string.Empty;
            Employee.gender = "male";
        }
        public void SearchEmployee(string name)
        {
            try
            {
                Employee.Employees = new ObservableCollection<Employee>();
                _repository.GetEmployeesSearch(name).ForEach(data => Employee.Employees.Add(new Employee()
                {
                    Id = data.Id,
                    name = data.name,
                    gender = data.gender,
                    status = data.status,
                    email = data.email
                }));


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while saving. " + ex.InnerException);
            }
            finally
            {
              //  GetAll();
            }
        }
        public void DeleteEmployee(Employee emp)
        {
            if (MessageBox.Show("Confirm delete of this record?", "User", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                try
                {
                    _repository.RemoveEmployee(emp.Id);
                    MessageBox.Show("Record successfully deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    GetAll();
                }
            }
        }

        public void SaveData(Employee emp)
        {
            if (Employee != null)
            {
                Employee.name = emp.name;
                Employee.gender = emp.gender;
                Employee.email = emp.email;
                Employee.status = emp.status;

                if (emp.name == "" || emp.name == null)
                {
                    Employee.errorname = "Please enter name field.";
                    return;
                }
                else Employee.errorname = "";
                if (emp.email == "" || emp.email == null)
                {
                    Employee.erroremail = "Please enter email field.";
                    return;
                }
                else Employee.erroremail = "";

                bool isValidEmail = Regex.IsMatch(Employee.email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if(!isValidEmail)
                {
                    Employee.erroremail = "Please enter a valid email."; 
                    return;
                }
                
                try
                {
                    if (emp.Id <= 0)
                    {
                        _repository.SaveEmployee(Employee);
                        MessageBox.Show("New record successfully saved.");

                    }
                    else
                    {
                        Employee.Id = emp.Id;
                        _repository.UpdateEmployee(Employee);
                        MessageBox.Show("Record successfully updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    GetAll();
                    ResetData();
                }
            }
        }

        public void EditData(int id)
        {
            AddEmployee w = new AddEmployee(id);
            w.Name = "AddEmployee";
            w.Show();
        }

        public void GetAll(int page =1)
        {
            Employee.Employees = new ObservableCollection<Employee>();
            _repository.GetEmployeesAsync(page).ForEach(data => Employee.Employees.Add(new Employee()
            {
                Id = data.Id,
                name = data.name,
                gender = data.gender,
                status = data.status,
                email = data.email
            }));
        }

        public List<Employee> GetEmployees()
        {
            // Employee.Employees = new ObservableCollection<Employee>();
            return _repository.GetAllEmployeesAsync();

           
        }
    }
}
