using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPS.Model;

namespace UPS.Repository
{
    public interface IEmployeeRepository
    {

        List<Employee> GetEmployeesAsync(int page = 1);
        void RemoveEmployee(int id);
    }
}
