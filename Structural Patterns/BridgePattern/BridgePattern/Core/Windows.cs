using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePattern.Core
{
    internal class Windows : IOperatingSystem
    {
        public void DoOperation()
        {
            Console.WriteLine("Doing operations on windows platform");
        }
    
    }
}
