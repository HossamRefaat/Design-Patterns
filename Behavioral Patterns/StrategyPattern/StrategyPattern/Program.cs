using StrategyPattern.Core;
using StrategyPattern.Core.DiscountStrategies;

namespace StrategyPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dataReader = new CustomerDataReader();
            var customers = dataReader.GetCustomers();
            while (true)
            {
                Console.WriteLine("Customer List: [1] Alice [2] Bob [3] Charlie");
                Console.WriteLine("Enter Customer ID");
                var input = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the quantity");
                var quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Unit Price");
                var unitPrice = int.Parse(Console.ReadLine());

                var customer = customers.FirstOrDefault(c => c.Id == input);

                ICustomerDiscountStrategy customerDiscountStrategy = null;
                if(customer.Category == CustomerCategory.Silver)
                {
                    customerDiscountStrategy = new SilverCustomerDiscountStrategy();
                }
                else if (customer.Category == CustomerCategory.Gold)
                {
                    customerDiscountStrategy = new GoldCustomerDiscountStrategy();
                }
                else
                {
                    customerDiscountStrategy = new NewCustomerDiscountStrategy();
                }
                
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
