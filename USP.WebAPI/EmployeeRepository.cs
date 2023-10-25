using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using UPS.Model;

namespace UPS.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Employee Get(int id)

        {
            UPS.WrapperApi.ApiHelper api = new UPS.WrapperApi.ApiHelper();

            HttpResponseMessage message = api.GetAPI("https://gorest.co.in/public/v2/users/" + id);
            var content = message.Content.ReadAsStringAsync();

            Employee emp = new Employee();
            var model = JsonConvert.DeserializeObject<Employee>(content.Result);

            return model;

        }
        public List<Employee> GetEmployeesAsync(int page = 1)
        {

            UPS.WrapperApi.ApiHelper api = new UPS.WrapperApi.ApiHelper();

            HttpResponseMessage message = api.GetAPI("https://gorest.co.in/public/v2/users?page=" + page);
            var content = message.Content.ReadAsStringAsync();


            List<Employee> olstemployee = new List<Employee>();
            var model = JsonConvert.DeserializeObject<List<Employee>>(content.Result);

            return model;
        }

        public List<Employee> GetAllEmployeesAsync()
        {

            UPS.WrapperApi.ApiHelper api = new UPS.WrapperApi.ApiHelper();

            HttpResponseMessage message = api.GetAPI("https://gorest.co.in/public/v2/users");
            var content = message.Content.ReadAsStringAsync();

            List<Employee> olstemployee = new List<Employee>();
            var model = JsonConvert.DeserializeObject<List<Employee>>(content.Result);

            return model;
        }
        public List<Employee> GetEmployeesSearch(string name)
        {

            UPS.WrapperApi.ApiHelper api = new UPS.WrapperApi.ApiHelper();

            HttpResponseMessage message = api.GetAPI("https://gorest.co.in/public/v2/users?name=" + name);
            var content = message.Content.ReadAsStringAsync();

            List<Employee> olstemployee = new List<Employee>();
            var model = JsonConvert.DeserializeObject<List<Employee>>(content.Result);

            return model;
        }
        public void RemoveEmployee(int id)
        {
            UPS.WrapperApi.ApiHelper api = new UPS.WrapperApi.ApiHelper();
            api.DeleteUser("https://gorest.co.in/public/v2/users/", id);

        }

        public void UpdateEmployee(Employee uEmp)
        {
            UPS.WrapperApi.ApiHelper api = new UPS.WrapperApi.ApiHelper();
            string s = JsonConvert.SerializeObject(uEmp);
            api.UpdateEmployee("https://gorest.co.in/public/v2/users/"+ uEmp.Id, s);

        }

        public void SaveEmployee(Employee emp)
        {
            Employee newEmp = new Employee();
            newEmp.name = emp.name;
            newEmp.email = emp.email;
            newEmp.status = emp.status;
            newEmp.gender = emp.gender;



            UPS.WrapperApi.ApiHelper api = new UPS.WrapperApi.ApiHelper();

            string s = JsonConvert.SerializeObject(newEmp);
            api.PostAPI("https://gorest.co.in//public/v2/users", s);


        }
    }
}
