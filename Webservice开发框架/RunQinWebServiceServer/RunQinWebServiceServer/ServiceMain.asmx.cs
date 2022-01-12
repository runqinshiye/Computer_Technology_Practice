using RunQinBusiness.Remoting;
using System;
using System.Web.Services;

namespace RunQinWebServiceServer
{
    /// <summary>
    /// 潤沁實嶪WebService编程框架
    /// </summary>
    [WebService(Namespace = "http://www.hagedu.com/RunQinWebServiceServer")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ServiceMain : System.Web.Services.WebService
    {

        [WebMethod(Description = "测试此接口系统搭建是否成功", EnableSession = false)]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(Description = "测试此接口系统接收参数是否成功", EnableSession = false)]
        public string hagedu(string para)
        {
            return $"由牙醫创造，为牙醫服务-{para}";
            //throw new NotSupportedException("Dto type not supported: " + para);

        }

        [WebMethod(Description = "WebService服务统一入口方法", EnableSession = false)]
        public string ProcessRequest(string dtoString)
        {
            return DtoProcessor.ProcessDto(dtoString, Server.MapPath("."));
        }
    }
}
