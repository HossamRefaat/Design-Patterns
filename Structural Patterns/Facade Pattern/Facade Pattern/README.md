# Facade Pattern Implementation

## What is the Facade Pattern?

The **Facade Pattern** is a structural design pattern that provides a simplified interface to a complex subsystem. It defines a higher-level interface that makes the subsystem easier to use by hiding the complexities of the underlying components.

### Key Characteristics:
- **Simplifies complex interfaces** by providing a unified entry point
- **Reduces coupling** between client code and subsystem components
- **Provides a single point of control** for coordinating multiple subsystems
- **Doesn't eliminate the underlying complexity** - it just hides it from the client

## The Problem We Faced

### Original Code Issues

Our original `Program.cs` had several problems that made it difficult to maintain and extend:

#### 1. **Complex Client Code**
```csharp
// Client had to manage multiple subsystems directly
var customers = new CustomerDataReader().GetCustomers();
var items = new ItemDataReader().GetItems();

// Complex display logic mixed with business logic
Console.WriteLine("Customer List:");
foreach(var c in customers)
{
    Console.WriteLine($"\t{c.Id}. {c.Name} ({c.Category})");
}

// Manual shopping cart type selection
ShoppingCart shoppingCart = Console.ReadLine().Equals("Online", StringComparison.OrdinalIgnoreCase)
    ? new OnlineShoppingCart()
    : new InStoreShoppingCart();

// Direct item lookup and validation
var item = items.First(x => x.Id == itemId);
shoppingCart.AddItem(itemId, quantity, item.UnitPrice);
```

#### 2. **Tight Coupling**
- `Program.cs` directly depended on 6+ classes
- Client code needed to know about internal implementations
- Changes to subsystems would require changes to client code

#### 3. **Scattered Responsibilities**
- Data loading mixed with UI logic
- Error handling scattered throughout the main method
- No centralized state management

#### 4. **Unused Complexity**
```csharp
// Commented code showed alternative approach that wasn't being used
//var customerDiscountStrategyFactory = new CustomerDiscountStrategyFactory();
//ICustomerDiscountStrategy customerDiscountStrategy = customerDiscountStrategyFactory.CreateCustomerDiscountStrategy(customer.Category);
//var invoiceManager = new InvoiceManager();
//invoiceManager.SetDiscountStrategy(customerDiscountStrategy);
```

## How We Solved It with the Facade Pattern

### Solution Architecture

We created an `EcommerceFacade` class that acts as a single entry point to our complex e-commerce subsystem:

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────────┐
│                 │    │                 │    │                     │
│   Program.cs    │───▶│ EcommerceFacade │───▶│   Complex Subsystem │
│   (Client)      │    │                 │    │                     │
└─────────────────┘    └─────────────────┘    └─────────────────────┘
```

### Key Components of Our Facade

#### 1. **EcommerceFacade Class**
```csharp
public class EcommerceFacade
{
    private readonly CustomerDataReader _customerDataReader;
    private readonly ItemDataReader _itemDataReader;
    private readonly CustomerDiscountStrategyFactory _discountStrategyFactory;
    
    // State management
    private Customer _selectedCustomer;
    private ShoppingCart _currentShoppingCart;
    private bool _isOrderActive;
    
    // Simplified public methods
    public void StartNewOrder()
    public bool SelectCustomer(int customerId)
    public bool SelectShoppingCartType(string cartType)
    public bool AddItemToCart(int itemId, int quantity)
    public bool CompleteOrder()
}
```

#### 2. **Simplified Client Code**
```csharp
// Before: 76 lines of complex coordination
// After: Clean, simple method calls
var ecommerceFacade = new EcommerceFacade();

while (true)
{
    ecommerceFacade.StartNewOrder();
    
    // Get customer selection
    if (!ecommerceFacade.SelectCustomer(customerId))
        continue;
    
    // Get shopping cart type
    if (!ecommerceFacade.SelectShoppingCartType(cartType))
        continue;
    
    // Add items to cart
    ecommerceFacade.AddItemToCart(itemId, quantity);
    
    // Complete order
    ecommerceFacade.CompleteOrder();
}
```

## Before vs After Comparison

| **Aspect** | **Before (Original)** | **After (With Facade)** |
|------------|----------------------|-------------------------|
| **Lines of Code** | 76 lines | 60 lines |
| **Dependencies** | 6+ direct class dependencies | 1 facade dependency |
| **Complexity** | High - mixed concerns | Low - single responsibility |
| **Error Handling** | Scattered try-catch | Centralized in facade |
| **State Management** | Manual coordination | Managed by facade |
| **Extensibility** | Hard to extend | Easy to add new features |

## Benefits Achieved

### 1. **Simplified Interface**
- **StartNewOrder()**: One method handles all initialization
- **SelectCustomer()**: Validates and manages customer selection
- **AddItemToCart()**: Handles item lookup and validation automatically

### 2. **Reduced Coupling**
- Client only depends on `EcommerceFacade`
- Subsystem changes don't affect client code
- Easy to swap implementations

### 3. **Centralized Control**
- All business logic coordinated in one place
- Consistent error handling
- State management handled internally

### 4. **Better Error Handling**
```csharp
// Facade provides consistent error handling
if (!ecommerceFacade.SelectCustomer(customerId))
{
    continue; // Error message already displayed by facade
}
```

### 5. **Future Flexibility**
- Easy to integrate the commented `InvoiceManager` logic
- Can add new features without changing client code
- Ready for new cart types or payment methods

## Implementation Details

### What Each Facade Method Does

#### `StartNewOrder()`
- Resets order state (`_selectedCustomer`, `_currentShoppingCart`)
- Loads fresh data from `CustomerDataReader` and `ItemDataReader`
- Displays formatted customer and item lists
- Sets `_isOrderActive = true`

#### `SelectCustomer(int customerId)`
- Validates order is active
- Looks up customer by ID
- Stores selected customer in `_selectedCustomer`
- Provides user feedback

#### `SelectShoppingCartType(string cartType)`
- Validates customer is selected
- Creates appropriate cart type (`OnlineShoppingCart` or `InStoreShoppingCart`)
- Stores cart in `_currentShoppingCart`

#### `AddItemToCart(int itemId, int quantity)`
- Validates cart is available
- Looks up item by ID
- Validates quantity
- Adds item to cart with proper pricing

#### `CompleteOrder()`
- Validates all required components are ready
- Calls `shoppingCart.Checkout(selectedCustomer)`
- Resets order state
- Handles exceptions gracefully

## How to Run

1. **Build the project:**
   ```bash
   dotnet build
   ```

2. **Run the application:**
   ```bash
   dotnet run
   ```

3. **Follow the prompts:**
   - Select a customer by ID
   - Choose cart type (Online/InStore)
   - Add items by ID and quantity
   - Enter 0 to complete the order

## Design Pattern Benefits Demonstrated

✅ **Simplified Interface**: Complex subsystem hidden behind simple methods  
✅ **Loose Coupling**: Client depends only on facade, not individual subsystems  
✅ **Centralized Logic**: All coordination handled in one place  
✅ **Better Maintainability**: Changes to subsystems don't affect client code  
✅ **Consistent Error Handling**: Unified approach to validation and errors  
✅ **State Management**: Facade manages order workflow state  

## Conclusion

The Facade Pattern transformed our complex, tightly-coupled e-commerce system into a clean, maintainable solution. By providing a simplified interface to our subsystems, we achieved better separation of concerns, reduced coupling, and improved the overall architecture of our application.

The pattern is particularly effective when dealing with complex subsystems that need to be coordinated, making it an excellent choice for e-commerce applications where multiple components (customers, items, carts, payments) need to work together seamlessly. 