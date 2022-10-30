using RestContract;
using System;
using System.Linq;
using System.ServiceModel;

namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ChannelFactory<IEmployees> channelFactory = new ChannelFactory<IEmployees>("employeeService"))
            {
                // 创建代理类
                IEmployees proxy = channelFactory.CreateChannel();

                Console.WriteLine("所有员工信息：");

                // 通过代理类来对Rest服务进行操作
                Array.ForEach<Employee>(proxy.GetAll().ToArray(), emp => Console.WriteLine(emp.ToString()));

                Console.WriteLine("\n添加一个新员工{0003}:");
                proxy.Create(new Employee
                {
                    Id = "0003", Name="李四", Department="财务部", Grade="G7"
                });

                Array.ForEach<Employee>(proxy.GetAll().ToArray(), emp => Console.WriteLine(emp.ToString()));

                Console.WriteLine("\n修改员工（0003）信息:");
                proxy.Update(new Employee 
                {
                    Id = "0003", Name="李四", Department = "销售部", Grade ="G8"
                });
                Array.ForEach<Employee>(proxy.GetAll().ToArray(), emp => Console.WriteLine(emp.ToString()));
                Console.WriteLine("\n删除员工(0002)信息：");
                proxy.Delete("0002");
                Array.ForEach<Employee>(proxy.GetAll().ToArray(), emp => Console.WriteLine(emp.ToString()));

                Console.Read();
            }
        }
    }
}
