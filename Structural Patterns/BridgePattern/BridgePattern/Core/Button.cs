using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePattern.Core
{
    internal class Button : CommonUI
    {
        public Button(IOperatingSystem operatingSystem) : base(operatingSystem)
        {
            
        }
       
        public override void Click()
        {
            operatingSystem.DoOperation();
        }
    }
}
