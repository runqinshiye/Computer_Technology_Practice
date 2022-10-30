using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BusinessEntity;

namespace WCFServiceAndHost
{
    // 服务契约的实现
    public class UserValidationService : IUserValidationService
    {
        public bool AddNewUser(User user)
        {
            return true;
        }

        public User GetUserByName(string name)
        {
            User user = new User { Name = name, Password = "123", Email = "123456@qq.com", Mobile = "13912331245" };
            return user;
        }

        // 演示已知类型的操作方法
        //public bool AddOrder(OrderBase order)
        //{
        //    return true;
        //}
    }
}
