using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsoleApp
{
    public class CompleteInstrumentationClient:SimpleInstrumentationClient, ICompleteInstrumentation
    {
        public string IncreatePerformanceCounter()
        {
            return this.Channel.IncreatePerformanceCounter();
        }
    }
}
