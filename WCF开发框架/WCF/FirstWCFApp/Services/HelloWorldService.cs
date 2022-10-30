using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract;

namespace Services
{
    public class HelloWorldService : IHelloWorld
    {
        public string GetHelloWorld()
        {
            return "Hello World";
        }
    }
}
