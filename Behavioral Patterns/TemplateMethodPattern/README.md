# Template Method Pattern: Shopping Cart Example

## What is the Template Method Pattern?

The Template Method pattern is a behavioral design pattern that defines the skeleton of an algorithm in a base class, but lets subclasses change certain steps of the algorithm without changing its structure.

**In simple terms:**
You write the main steps of a process once in a base class, and let child classes fill in the details for some steps.

## How This Project Works

### Step-by-Step Process
1. **Select Customer**: Choose from Alice (Gold), Bob (Silver), or Lord Voldemort (None)
2. **Choose Cart Type**: Select between "Online" or "InStore" shopping cart
3. **Add Items**: Add multiple items to the cart (Laptop, LCD, Keyboard, Mouse)
4. **Complete Order**: Enter 0 to finish adding items
5. **Checkout**: The system processes the order using the Template Method pattern

### Example Output
- **Online Cart**: includes 5% discount for orders over 10,000
- **InStore Cart**: no discount applied

## How the Template Method Pattern is Implemented

### The Template Method Pattern in Action

```csharp
abstract class ShoppingCart
{
    // THIS IS THE TEMPLATE METHOD - defines the algorithm skeleton
    public void Checkout(Customer customer)
    {
        var invoice = new Invoice
        {
            Customer = customer,
            Lines = lines
        };

        ApplyTaxes(invoice);        // Fixed step (15% tax)
        ApplyDiscount(invoice);     // ABSTRACT STEP - customizable by subclasses
        ProcessPayment(invoice);    // Fixed step (display result)
    }

    // ABSTRACT METHOD - this is what makes it a Template Method pattern
    abstract protected void ApplyDiscount(Invoice invoice);
    
    private void ApplyTaxes(Invoice invoice)
    {
        invoice.Taxes = invoice.TotalPrice * 0.15;
    }
}
```

**Key Pattern Elements:**
- **Template Method**: `Checkout()` - defines the algorithm structure
- **Abstract Method**: `ApplyDiscount()` - the customizable step
- **Fixed Methods**: `ApplyTaxes()` and `ProcessPayment()` - same for all subclasses

### The Concrete Implementations

#### OnlineShoppingCart
```csharp
class OnlineShoppingCart : ShoppingCart
{
    protected override void ApplyDiscount(Invoice invoice)
    {
        if (invoice.TotalPrice > 10000)
            invoice.DiscountPercentage = 0.05; // 5% discount
    }
}
```

#### InStoreShoppingCart
```csharp
class InStoreShoppingCart : ShoppingCart
{
    protected override void ApplyDiscount(Invoice invoice)
    {
        // No discount for in-store purchases
    }
}
```
## Why This Pattern is Useful

### ✅ Advantages
1. **Code Reuse**: The checkout process is written once in the base class
2. **Consistency**: All shopping carts follow the same checkout steps
3. **Flexibility**: Each cart type can customize only the discount logic
4. **Maintainability**: Changes to the checkout process only need to be made in one place
5. **Extensibility**: Easy to add new cart types (e.g., MobileShoppingCart)

The Template Method pattern ensures that the core algorithm stays consistent while allowing customization of specific steps! 