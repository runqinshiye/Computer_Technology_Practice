using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ClientConsoleApp
{
    // 自定义代理类
    public class SimpleInstrumentationClient : ClientBase<ICompleteInstrumentation>, ISimpleInstrumentation
    {
        #region ISimpleInstrumentation Members
        public string WriteEventLog() 
        {
            return this.Channel.WriteEventLog();
        }
        #endregion 
    }
}
