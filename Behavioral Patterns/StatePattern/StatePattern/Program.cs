using StatePattern.Core;

namespace StatePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var order = new Order();

            Console.WriteLine("Status: " + order.GetStatus());
            order.NextStep(); // New -> Processing
            Console.WriteLine("Status: " + order.GetStatus());
            order.NextStep(); // Processing -> Shipped
            Console.WriteLine("Status: " + order.GetStatus());
            order.CancelOrder(); // Can't cancel a shipped order
            order.NextStep(); // Shipped -> Delivered
            Console.WriteLine("Status: " + order.GetStatus());
        }
    }
}
