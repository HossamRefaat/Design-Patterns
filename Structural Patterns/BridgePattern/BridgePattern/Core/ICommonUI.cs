using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgePattern.Core
{
    abstract class CommonUI
    {
        protected IOperatingSystem operatingSystem;

        protected CommonUI(IOperatingSystem operatingSystem)
        {
            this.operatingSystem = operatingSystem;
        }

        public abstract void Click();
    }
}
