using System;
using System.Web.Services;
using System.Web.Services.Protocols;
using WebServiceUserValidation;

namespace WebService
{
    /// <summary>
    /// LearningHardWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.cnblogs.com/zhili/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class LearningHardWebService : System.Web.Services.WebService
    {
        // 存储用户凭证的Soap Header信息
        // 必须保证是public和字段名必须与SoapHeader("memberName")中memberName一样
        // 否则会出现“头属性/字段 LearningHardWebService.authenticationToken 缺失或者不是公共的。”的异常
        public MySoapHeader authenticationToken; 
        private const string TOKEN = "LearningHard"; // 存储服务器端凭证
        

        // 定义SoapHeader传递的方向
        //SoapHeaderDirection.In;只发送SoapHeader到服务端,该值是默认值
        //SoapHeaderDirection.Out;只发送SoapHeader到客户端
        //SoapHeaderDirection.InOut;发送SoapHeader到服务端和客户端
        //SoapHeaderDirection.Fault;服务端方法异常的话，会发送异常信息到客户端
        [SoapHeader("authenticationToken", Direction = SoapHeaderDirection.InOut)]
        [WebMethod(EnableSession = false)]
        public string HelloLearningHard()
        {
            if (authenticationToken != null && UserValidation.IsUserLegal(authenticationToken.Token))
            {
                return "LearningHard 你好，调用服务方法成功!";
            }
            else
            {
                throw new SoapException("身份验证失败", SoapException.ServerFaultCode);
            }
        }
    }
}
