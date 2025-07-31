using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Core
{
    internal class OrderProcessorQueuingDecorator : IOrderProcessor
    {
        private readonly IOrderProcessor orderProcessor;
        private Queue<Order> orders = new();

        public OrderProcessorQueuingDecorator(IOrderProcessor orderProcessor)
        {
            this.orderProcessor = orderProcessor;
        }
        public void Process(Order order)
        {
            orders.Enqueue(order);
            Console.WriteLine("Order has been queued.");
        }
    }
}
