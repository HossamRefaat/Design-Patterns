using FactoryMethodPattern.Core;
using FactoryMethodPattern.Core.DiscountStrategies;
using FactoryMethodPattern.Core.Payments;
using FactoryMethodPattern.Core.ShoppingCarts;
using FactoryMethodPattern.Payments.Abstractions;

namespace FactoryMethodPattern
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

                Console.WriteLine("Select payment method (Visa | Paypal): ");
                var paymentInput = Console.ReadLine()?.Trim() ?? "";
                PaymentProcessor paymentProcessor = paymentInput.Equals("Visa", StringComparison.OrdinalIgnoreCase) ? new VisaPaymentProcessor() : new PayPalPaymentProcessor();
                shoppingCart.Checkout(selectedCustomer, paymentProcessor);

                //Console.WriteLine($"Invoice created for the customer `{customer.Name}` with net price: {invoice.NetPrice}");

                Console.WriteLine("Press any key to create another invoice");
                Console.ReadKey();
                Console.WriteLine("---------------------------------------------");
            }
        }
    }
}
