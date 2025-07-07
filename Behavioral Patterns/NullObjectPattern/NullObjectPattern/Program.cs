using NullObjectPattern.Core;
using NullObjectPattern.Core.DiscountStrategies;

namespace NullObjectPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dataReader = new CustomerDataReader();
            var customers = dataReader.GetCustomers();
            while (true)
            {
                Console.WriteLine("Customer List:");
                foreach(var c in customers)
                {
                    Console.WriteLine($"\t{c.Id}. {c.Name} ({c.Category})");
                }
                Console.WriteLine();
                 
                Console.WriteLine("Enter Customer ID");
                var input = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the quantity");
                var quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Unit Price");
                var unitPrice = int.Parse(Console.ReadLine());

                var customer = customers.FirstOrDefault(c => c.Id == input);

                if (customer == null)
                {
                    Console.WriteLine("Invalid Customer ID. Please try again.");
                    continue;
                }

                var customerDiscountStrategyFactory = new CustomerDiscountStrategyFactory();
                ICustomerDiscountStrategy customerDiscountStrategy = customerDiscountStrategyFactory.CreateCustomerDiscountStrategy(customer.Category);

                var invoiceManager = new InvoiceManager();
                invoiceManager.SetDiscountStrategy(customerDiscountStrategy);

                var invoice = invoiceManager.CreateInvoice(customer, quantity, unitPrice);

                Console.WriteLine($"Invoice created for the customer `{customer.Name}` with net price: {invoice.NetPrice}");
                Console.WriteLine("Press any key to create another invoice");
                Console.ReadKey();
                Console.WriteLine("---------------------------------------------");
            }
        }
    }
}
