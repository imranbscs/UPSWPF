using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UPS.Model;
using UPS.Repository;
using UPSWPPF.ViewModel;

namespace UPSWPF
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        private EmployeeRepository _repository;
        public AddEmployee()
        {
            InitializeComponent();
            _repository = new EmployeeRepository();
            this.DataContext = new EmployeeViewModel(1); // FOR PAGE NO. 1

            cmbGender.Items.Clear();
            cmbGender.ItemsSource = new List<string>() { "male", "female" };
            cmbGender.SelectedItem = "male" ;


            cmbStatus.Items.Clear();
            cmbStatus.ItemsSource = new List<string>() { "active", "Inactive" };
            cmbStatus.SelectedItem = "active" ;

        }
        public AddEmployee(int id)
        {
            InitializeComponent();
            _repository = new EmployeeRepository();
            this.DataContext = new EmployeeViewModel(1); // FOR PAGE NO. 1
            var model = _repository.Get(id);
            ((EmployeeViewModel)this.DataContext).Employee.name = model.name;
            ((EmployeeViewModel)this.DataContext).Employee.email = model.email;
            ((EmployeeViewModel)this.DataContext).Employee.Id = id;
            ((EmployeeViewModel)this.DataContext).Employee.gender = model.gender;
            ((EmployeeViewModel)this.DataContext).Employee.status = model.status;

            cmbGender.Items.Clear();
            cmbGender.ItemsSource = new List<string>() { "male", "female" };
            cmbGender.SelectedItem = model.gender; 


            cmbStatus.Items.Clear();
            cmbStatus.ItemsSource = new List<string>() { "active", "inactive" };
            cmbStatus.SelectedItem = model.status; 
            
        }

        
        private void male_Selected(object sender, RoutedEventArgs e)
        {
           
                var item = cmbGender.SelectedValue;
                if (this.DataContext != null && cmbGender.SelectedValue != null)
                    ((EmployeeViewModel)this.DataContext).Employee.gender = (string)item;
            
        }

        private void status_Selected(object sender, RoutedEventArgs e)
        {

            var item = cmbStatus.SelectedValue;
            if (this.DataContext != null && cmbStatus.SelectedValue != null)
                ((EmployeeViewModel)this.DataContext).Employee.status = (string)item;
        }
    }

}
