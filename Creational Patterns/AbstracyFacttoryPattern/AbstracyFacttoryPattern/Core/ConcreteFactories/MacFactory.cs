using AbstracyFacttoryPattern.Core.AbstractProducts;
using AbstracyFacttoryPattern.Core.ConcreteProducts.ConcreteProductsWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstracyFacttoryPattern.Core.ConcreteFactories
{
    internal class MacFactory : IGUIFactory
    {
        public IButton CreateButton() => new MacButton();
        public ICheckbox CreateCheckbox() => new MacCheckbox();

    }
}
