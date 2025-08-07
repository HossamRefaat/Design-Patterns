using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePattern.Core
{
    internal class Linux : IOperatingSystem
    {
        public void DoOperation()
        {
            Console.WriteLine("Doing operations on linux platform");
        }
    }
}
