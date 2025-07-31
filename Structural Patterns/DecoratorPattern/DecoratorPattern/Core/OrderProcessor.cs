using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Core
{
    internal class OrderProcessor : IOrderProcessor
    {
        public virtual void Process(Order order)
        {
            if(order.Lines.Count() == 0)
                throw new InvalidOperationException("Cannot process an order with no lines.");

            Thread.Sleep(Random.Shared.Next(1000, 3000)); // Simulate processing time
            Console.WriteLine("Order has been processed");
        }
    }
}
