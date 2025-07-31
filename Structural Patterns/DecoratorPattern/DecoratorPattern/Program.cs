using DecoratorPattern.Core;

namespace DecoratorPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var order = new Order();
            order.AddLine(1, 2, 10.00m);
            order.AddLine(2, 1, 20.00m);
            order.AddLine(3, 5, 5.00m);

            IOrderProcessor orderProcessor = new OrderProcessor();
            orderProcessor = new OrderProcessorExceptionHandlingDecorator(orderProcessor);
            orderProcessor = new OrderProcessorProfilingDecorator(orderProcessor);
            orderProcessor = new OrderProcessorQueuingDecorator(orderProcessor);
            orderProcessor.Process(order);

            Console.ReadKey();
        }
    }
}
