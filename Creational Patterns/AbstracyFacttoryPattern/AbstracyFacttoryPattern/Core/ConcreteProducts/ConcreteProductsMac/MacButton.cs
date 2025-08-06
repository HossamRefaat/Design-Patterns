using AbstracyFacttoryPattern.Core.AbstractProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstracyFacttoryPattern.Core.ConcreteProducts.ConcreteProductsWindows
{
    internal class MacButton : IButton
    {
        public void Render() => Console.WriteLine("Rendering Mac Button.");
    }
}
