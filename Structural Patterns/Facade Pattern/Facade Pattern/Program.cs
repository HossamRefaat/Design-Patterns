using FacadePattern.Core;

namespace FacadePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ecommerceFacade = new EcommerceFacade();

            while (true)
            {
                try
                {
                    // Start a new order - facade handles all initialization
                    ecommerceFacade.StartNewOrder();

                    // Get customer selection
                    Console.WriteLine("Enter Customer ID:");
                    if (!int.TryParse(Console.ReadLine(), out int customerId))
                    {
                        Console.WriteLine("Invalid customer ID format.");
                        continue;
                    }

                    if (!ecommerceFacade.SelectCustomer(customerId))
                    {
                        continue; // Error message already displayed by facade
                    }

                    // Get shopping cart type selection
                    Console.WriteLine("Select shopping cart type (Online | InStore):");
                    string cartType = Console.ReadLine();

                    if (!ecommerceFacade.SelectShoppingCartType(cartType))
                    {
                        continue; // Error message already displayed by facade
                    }

                    // Add items to cart
                    while (true)
                    {
                        Console.WriteLine("Enter Item ID (0 to complete the order):");
                        if (!int.TryParse(Console.ReadLine(), out int itemId))
                        {
                            Console.WriteLine("Invalid item ID format.");
                            continue;
                        }

                        if (itemId == 0)
                        {
                            break; // Complete the order
                        }

                        Console.WriteLine("Enter Item Quantity:");
                        if (!int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            Console.WriteLine("Invalid quantity format.");
                            continue;
                        }

                        // Facade handles all the complexity of finding items and adding to cart
                        ecommerceFacade.AddItemToCart(itemId, quantity);
                    }

                    // Complete the order - facade handles checkout complexity
                    if (ecommerceFacade.CompleteOrder())
                    {
                        Console.WriteLine("Order completed successfully!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                Console.WriteLine("Press any key to create another invoice");
                Console.ReadKey();
                Console.WriteLine("---------------------------------------------");
            }
        }
    }
}
