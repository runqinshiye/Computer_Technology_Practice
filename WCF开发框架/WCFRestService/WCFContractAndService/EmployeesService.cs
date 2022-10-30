using RestContract;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;

namespace WCFContractAndService
{
    public class EmployeesService : IEmployees
    {
        private static IList<Employee> employees = new List<Employee>
        {
            new Employee{ Id = "0001", Name = "LearningHard", Department = "开发部",Grade = "G6"},
            new Employee{Id = "0002", Name = "张三", Department = "QA", Grade = "G5"}
        };

        public Employee Get(string id)
        {
            Employee employee = employees.FirstOrDefault(e => e.Id == id);
            if (null == employee)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            return employees;
        }

        public void Create(Employee employee)
        {
            employees.Add(employee);
        }


        public void Update(Employee emp)
        {
            this.Delete(emp.Id);
            employees.Add(emp);
        }

        public void Delete(string id)
        {
            Employee employee = this.Get(id);
            if (null != employee)
            {
                employees.Remove(employee);
            }
        }
    }
}
