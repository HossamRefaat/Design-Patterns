using AbstracyFacttoryPattern.Core.AbstractProducts;
using AbstracyFacttoryPattern.Core.ConcreteProducts.ConcreteProductsWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstracyFacttoryPattern.Core.ConcreteFactories
{
    internal class WindowsFactory : IGUIFactory
    {
        public IButton CreateButton() => new WindowsButton();
        public ICheckbox CreateCheckbox() => new WindowsCheckbox();

    }
}
