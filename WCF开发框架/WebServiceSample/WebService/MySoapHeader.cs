using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WebService
{
    // 用户自定义的SoapHeader类必须继承于SoapHeader
    public class MySoapHeader : SoapHeader
    {
        // 存储用户凭证
        public string Token { get; set; }
    }
}