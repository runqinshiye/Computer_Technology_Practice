using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService
{
    // 实现ICompleteInstrumentation契约
    public class CompleteInstrumentationService: SimpleInstrumentationService, ICompleteInstrumentation
    {
        public string IncreatePerformanceCounter()
        {
            return "Increate Performance Counter is called";
        }
    }
}
