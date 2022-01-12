using RunQinBusiness.Remoting;
using System.Net;

namespace RunQinBusiness.WebServices
{
    public class RunQinServerProxy
    {
        public static RunQinServerMockIIS MockRunQinServerCur
        {
            get;
            set;
        }

        public static IRunQinServer GetRunQinServerInstance()
        {
            if (MockRunQinServerCur != null)
            {
                return MockRunQinServerCur;
            }
            RunQinServerReal service = new RunQinServerReal();
            service.Url = RemotingClient.ServerURI;
            if (RemotingClient.MidTierProxyAddress != null && RemotingClient.MidTierProxyAddress != "")
            {
                IWebProxy proxy = new WebProxy(RemotingClient.MidTierProxyAddress);
                ICredentials cred = new NetworkCredential(RemotingClient.MidTierProxyUserName, RemotingClient.MidTierProxyPassword);
                proxy.Credentials = cred;
                service.Proxy = proxy;
            }
            service.Timeout = 3600000;
            service.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; MS Web Services Client Protocol 4.0.30319.296; IE8Mercury)";
            return service;
        }

    }
}
