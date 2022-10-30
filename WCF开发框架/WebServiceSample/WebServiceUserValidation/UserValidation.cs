using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceUserValidation
{
    public class UserValidation
    {
        // 判断用户名和密码是否有效
        public static bool IsUserLegal(string name, string psw)
        {
            // 用户可以访问数据库进行用户和密码验证
            // 这里仅仅作为演示
            string password = "LearningHard";
            if (string.Equals(password, psw))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        // 判断用户的凭证是否有效
        public static bool IsUserLegal(string token)
        {
            // 用户可以访问数据库进行用户凭证验证
            // 这里只做演示
            string password = "LearningHard";
            if (string.Equals(password, token))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
