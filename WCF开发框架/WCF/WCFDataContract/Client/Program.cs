using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ServiceReference;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            UserValidationServiceClient wcfServiceProxy = new UserValidationServiceClient();
            User newUser = new User() { UserName = "LearningHard", Email = "123456@qq.com", Password = "123" };
            wcfServiceProxy.AddNewUser(newUser);

            // 演示已知类型的问题
            //Order order = new Order() { ID = Guid.NewGuid(), Date = DateTime.Now, Customer = "customer1", ShipAddress = "Shanghai", TotalPrice = 20.00 };
            //wcfServiceProxy.AddOrder(order);

            // 获得用户信息
            string name = "Learning Hard Client";
            User user = wcfServiceProxy.GetUserByName(name);
            if (user != null)
            {
                Console.WriteLine("User Name is: " + user.UserName);
                Console.WriteLine("Email is: " + user.Email);
            }

            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }
    }
}
