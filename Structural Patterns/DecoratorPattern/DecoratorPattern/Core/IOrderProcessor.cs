using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Core
{
    internal interface IOrderProcessor
    {
        void Process(Order order);
    }
}
