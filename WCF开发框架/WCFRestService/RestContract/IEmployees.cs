using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RestContract
{
    [ServiceContract(Namespace ="http://www.cnblogs.com/zhili/")]
    public interface IEmployees
    {
        // 契约操作不再使用操作契约的方式来标识，而是使用WebGetAttribute特性来标识，从而表明该服务是Rest服务
        [WebGet(UriTemplate = "all")] 
        IEnumerable<Employee> GetAll();

        [WebGet(UriTemplate = "{id}")]
        Employee Get(string id);
        
        [WebInvoke(UriTemplate="/", Method="PUT")]
        void Create(Employee employee);

        [WebInvoke(UriTemplate = "/", Method = "POST")]
        void Update(Employee employee);

        [WebInvoke(UriTemplate = "/", Method = "DELETE")]
        void Delete(string id);
    }

    [DataContract(Namespace = "http://www.cnblogs.com.zhili/")]
    public class Employee
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public string Grade { get; set; }

        public override string ToString()
        {
            return string.Format("ID: {0,-5}姓名：{1,-5}部门：{2,-5}级别：{3}",Id, Name, Department, Grade);
        }
    }
}
