using FacadePattern.Core.DiscountStrategies;
using FacadePattern.Core.ShoppingCarts;

namespace FacadePattern.Core
{
    public class EcommerceFacade
    {
        private readonly CustomerDataReader _customerDataReader;
        private readonly ItemDataReader _itemDataReader;
        private readonly CustomerDiscountStrategyFactory _discountStrategyFactory;
        
        private List<Customer> _customers;
        private List<Item> _items;
        private Customer _selectedCustomer;
        private ShoppingCart _currentShoppingCart;
        private bool _isOrderActive;

        public EcommerceFacade()
        {
            _customerDataReader = new CustomerDataReader();
            _itemDataReader = new ItemDataReader();
            _discountStrategyFactory = new CustomerDiscountStrategyFactory();
            _isOrderActive = false;
        }

        public void StartNewOrder()
        {
            // Reset order state
            _selectedCustomer = null;
            _currentShoppingCart = null;
            _isOrderActive = true;

            // Load fresh data
            _customers = _customerDataReader.GetCustomers();
            _items = _itemDataReader.GetItems();

            // Display available options
            DisplayCustomers();
            DisplayItems();
        }

        public List<Customer> GetAvailableCustomers()
        {
            return _customers ?? new List<Customer>();
        }

        public List<Item> GetAvailableItems()
        {
            return _items ?? new List<Item>();
        }

        public bool SelectCustomer(int customerId)
        {
            if (!_isOrderActive)
            {
                Console.WriteLine("Please start a new order first.");
                return false;
            }

            var customer = _customers?.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                Console.WriteLine("Invalid customer ID.");
                return false;
            }

            _selectedCustomer = customer;
            Console.WriteLine($"Selected customer: {customer.Name} ({customer.Category})");
            return true;
        }

        public bool SelectShoppingCartType(string cartType)
        {
            if (!_isOrderActive || _selectedCustomer == null)
            {
                Console.WriteLine("Please select a customer first.");
                return false;
            }

            if (cartType.Equals("Online", StringComparison.OrdinalIgnoreCase))
            {
                _currentShoppingCart = new OnlineShoppingCart();
                Console.WriteLine("Online shopping cart selected.");
            }
            else if (cartType.Equals("InStore", StringComparison.OrdinalIgnoreCase))
            {
                _currentShoppingCart = new InStoreShoppingCart();
                Console.WriteLine("In-store shopping cart selected.");
            }
            else
            {
                Console.WriteLine("Invalid cart type. Please enter 'Online' or 'InStore'.");
                return false;
            }

            return true;
        }

        public bool AddItemToCart(int itemId, int quantity)
        {
            if (!_isOrderActive || _currentShoppingCart == null)
            {
                Console.WriteLine("Please select a shopping cart first.");
                return false;
            }

            var item = _items?.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                Console.WriteLine("Invalid item ID.");
                return false;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0.");
                return false;
            }

            _currentShoppingCart.AddItem(itemId, quantity, item.UnitPrice);
            Console.WriteLine($"Added {quantity} x {item.Name} to cart.");
            return true;
        }

        public bool CompleteOrder()
        {
            if (!_isOrderActive || _selectedCustomer == null || _currentShoppingCart == null)
            {
                Console.WriteLine("Cannot complete order. Please ensure customer and cart are selected.");
                return false;
            }

            try
            {
                _currentShoppingCart.Checkout(_selectedCustomer);
                
                // Reset order state
                _isOrderActive = false;
                _selectedCustomer = null;
                _currentShoppingCart = null;
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error completing order: {ex.Message}");
                return false;
            }
        }

        public bool IsOrderActive()
        {
            return _isOrderActive;
        }

        public Customer GetSelectedCustomer()
        {
            return _selectedCustomer;
        }

        public bool HasItemsInCart()
        {
            // This would require modifying ShoppingCart to expose line count
            // For now, we'll assume if cart is created, it can have items
            return _currentShoppingCart != null;
        }

        private void DisplayCustomers()
        {
            Console.WriteLine("Customer List:");
            foreach (var customer in _customers)
            {
                Console.WriteLine($"\t{customer.Id}. {customer.Name} ({customer.Category})");
            }
            Console.WriteLine();
        }

        private void DisplayItems()
        {
            Console.WriteLine("Item List:");
            foreach (var item in _items)
            {
                Console.WriteLine($"\t{item.Id}. {item.Name} ({item.UnitPrice:0.00})");
            }
            Console.WriteLine();
        }
    }
} 