using TemplateMethodPattern.Core;
using TemplateMethodPattern.Core.DiscountStrategies;
using TemplateMethodPattern.Core.ShoppingCarts;

namespace TemplateMethodPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var customers = new CustomerDataReader().GetCustomers();
            var items = new ItemDataReader().GetItems();

            while (true)
            {
                Console.WriteLine("Customer List:");
                foreach(var c in customers)
                {
                    Console.WriteLine($"\t{c.Id}. {c.Name} ({c.Category})");
                }
                Console.WriteLine();

                Console.WriteLine("Item List:");
                foreach (var t in items)
                {
                    Console.WriteLine($"\t{t.Id}. {t.Name} ({t.UnitPrice:0.00})");
                }
                Console.WriteLine();

                Console.WriteLine("Enter Customer ID");
                var customerId = int.Parse(Console.ReadLine());

                Console.WriteLine("Select shopping cart type (Online | InStore)");
                ShoppingCart shoppingCart = Console.ReadLine().Equals("Online", StringComparison.OrdinalIgnoreCase)
                    ? new OnlineShoppingCart()
                    : new InStoreShoppingCart();

                while (true)
                {
                    Console.WriteLine("Enter Item ID (0 to complete the order)");
                    var itemId = int.Parse(Console.ReadLine());

                    if (itemId == 0)
                    {
                        break; 
                    }

                    Console.WriteLine("Enter Item Quantity: ");
                    var quntity = int.Parse(Console.ReadLine());

                    var item = items.First(x => x.Id == itemId);
                    shoppingCart.AddItem(itemId, quntity, item.UnitPrice);
                }

                var selectedCustomer = customers.First(x => x.Id == customerId);

                //var customerDiscountStrategyFactory = new CustomerDiscountStrategyFactory();
                //ICustomerDiscountStrategy customerDiscountStrategy = customerDiscountStrategyFactory.CreateCustomerDiscountStrategy(customer.Category);

                //var invoiceManager = new InvoiceManager();
                //invoiceManager.SetDiscountStrategy(customerDiscountStrategy);

                //var invoice = invoiceManager.CreateInvoice(customer, quantity, unitPrice);

                shoppingCart.Checkout(selectedCustomer);

                //Console.WriteLine($"Invoice created for the customer `{customer.Name}` with net price: {invoice.NetPrice}");

                Console.WriteLine("Press any key to create another invoice");
                Console.ReadKey();
                Console.WriteLine("---------------------------------------------");
            }
        }
    }
}
