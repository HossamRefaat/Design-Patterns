using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Core
{
    internal class OrderProcessorExceptionHandlingDecorator : IOrderProcessor
    {
        private readonly IOrderProcessor orderProcessor;

        public OrderProcessorExceptionHandlingDecorator(IOrderProcessor orderProcessor)
        {
            this.orderProcessor = orderProcessor;
        }

        public void Process(Order order)
        {
            try
            {
                orderProcessor.Process(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while processing the order: {ex.Message}");
                // Here you could log the exception or take other actions as needed
            }
        }
    }
}
