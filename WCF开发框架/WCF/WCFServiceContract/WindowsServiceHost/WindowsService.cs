using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceHost
{
    public partial class WindowsService : ServiceBase
    {    
        public WindowsService()
        {
            InitializeComponent();
        }

        public ServiceHost serviceHost = null;
 
        // 启动Windows服务
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            serviceHost = new ServiceHost(typeof(Services.HelloWorldService));
            serviceHost.Open();
        }

        // 停止Windows服务
        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
