using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UPS.Model;
using UPSWPF.Util;
using UPSWPPF.ViewModel;

namespace UPSWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        private int page = 1;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(1);
        }
        private void OnNextClicked(object sender, RoutedEventArgs e)
        {
            page = page + 1;
            this.DataContext = new EmployeeViewModel(page);
           
        }
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
        private void OnPreviousClicked(object sender, RoutedEventArgs e)
        {
            page = page - 1;
            if (page <= 0)
                page = 1;
            this.DataContext = new EmployeeViewModel(page);
            
        }
       
        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            if (IsWindowOpen<Window>("AddEmployee"))
                return;

            AddEmployee w = new AddEmployee();
            w.Name = "AddEmployee";
            w.Show();
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            List<Employee> emp = ((EmployeeViewModel)this.DataContext).GetEmployees();
            DataTable dt =Helper.ConvertToDataTable(emp);
            dt.Columns.Remove("Employees");
            dt.Columns.Remove("errorname");
            dt.Columns.Remove("erroremail");
            Util.Helper.ConvertToCsvFile(dt, @"d:\\export\EmployeeDetails.csv");
            MessageBox.Show(@"File Exported to path d:\\export\\EmployeeDetails\EmployeeDetails.csv");
        }
    }

    public class PagingCollectionView : CollectionView
    {
        private readonly IList _innerList;
        private readonly int _itemsPerPage;

        private int _currentPage = 1;

        public PagingCollectionView(IList innerList, int itemsPerPage)
            : base(innerList)
        {
            this._innerList = innerList;
            this._itemsPerPage = itemsPerPage;
        }

        public override int Count
        {
            get
            {
                if (this._innerList.Count == 0) return 0;
                if (this._currentPage < this.PageCount) // page 1..n-1
                {
                    return this._itemsPerPage;
                }
                else // page n
                {
                    var itemsLeft = this._innerList.Count % this._itemsPerPage;
                    if (0 == itemsLeft)
                    {
                        return this._itemsPerPage; // exactly itemsPerPage left
                    }
                    else
                    {
                        // return the remaining items
                        return itemsLeft;
                    }
                }
            }
        }

        public int CurrentPage
        {
            get { return this._currentPage; }
            set
            {
                this._currentPage = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("CurrentPage"));
            }
        }

        public int ItemsPerPage { get { return this._itemsPerPage; } }

        public int PageCount
        {
            get
            {
                return (this._innerList.Count + this._itemsPerPage - 1)
                    / this._itemsPerPage;
            }
        }

        private int EndIndex
        {
            get
            {
                var end = this._currentPage * this._itemsPerPage - 1;
                return (end > this._innerList.Count) ? this._innerList.Count : end;
            }
        }

        private int StartIndex
        {
            get { return (this._currentPage - 1) * this._itemsPerPage; }
        }

        public override object GetItemAt(int index)
        {
            var offset = index % (this._itemsPerPage);
            return this._innerList[this.StartIndex + offset];
        }

        public void MoveToNextPage()
        {
            if (this._currentPage < this.PageCount)
            {
                this.CurrentPage += 1;
            }
            this.Refresh();
        }

        public void MoveToPreviousPage()
        {
            if (this._currentPage > 1)
            {
                this.CurrentPage -= 1;
            }
            this.Refresh();
        }


       
    }
}

