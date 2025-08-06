using AbstracyFacttoryPattern.Core.AbstractProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstracyFacttoryPattern.Core
{
    internal class Application
    {
        private IButton button;
        private ICheckbox checkbox;

        public Application(IGUIFactory factory)
        {
            button = factory.CreateButton();
            checkbox = factory.CreateCheckbox();
        }

        public void Run()
        {
            button.Render();
            checkbox.Render();
        }
    }
}
