using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService
{
    // 实现ISimpleInstrumentation契约
    public class SimpleInstrumentationService : ISimpleInstrumentation
    {
        #region ISimpleInstrumentation members
        public string WriteEventLog()
        {
           return "Simple Instrumentation Service is Called";
        }
        #endregion 
    }
}
