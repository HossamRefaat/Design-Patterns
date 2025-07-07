# Null Object Pattern: Customer Discount System

## What is the Null Object Pattern?

The Null Object pattern is a **behavioral design pattern** that provides a safe alternative to null references. Instead of returning null when no object is found, you return a special object that does nothing (or does something safe) when its methods are called.

**Simple Example:** Instead of returning `null` when a customer has no discount, we return a `NullDiscountStrategy` that always returns 0% discount.

## What This Project Does

This project demonstrates the **Null Object pattern** using a customer discount system. Some customers have discount categories (Gold, Silver), while others have no discount category (None). Instead of handling null cases, we use a Null Object.

### Customer Categories
- **Gold Customers**: 10% discount on orders over $10,000
- **Silver Customers**: 5% discount on orders over $10,000
- **None Category**: No discount (0%) - uses Null Object pattern

## How the Null Object Pattern is Used Here

### The Problem Without Null Object
Without the Null Object pattern, we might have to check for null:
```csharp
// Problematic approach
ICustomerDiscountStrategy strategy = GetStrategy(customer.Category);
if (strategy != null)
{
    discount = strategy.CalculateDiscount(totalPrice);
}
else
{
    discount = 0; // Handle null case
}
```

### The Solution With Null Object
We create a **NullDiscountStrategy** that always returns 0:
```csharp
public class NullDiscountStrategy : ICustomerDiscountStrategy
{
    public double CalculateDiscount(double totalPrice)
    {
        return 0; // Safe default behavior
    }
}
```

### The Factory Implementation
```csharp
public ICustomerDiscountStrategy CreateCustomerDiscountStrategy(CustomerCategory category)
{
    if (category == CustomerCategory.Silver)
        return new SilverCustomerDiscountStrategy();
    else if (category == CustomerCategory.Gold)
        return new GoldCustomerDiscountStrategy();
    else
        return new NullDiscountStrategy(); // Never returns null!
}
```

## Why This is Better

### ✅ Advantages of Null Object Pattern
1. **No Null Checks**: Never need to check for null
2. **Safe Default Behavior**: Always get a predictable result
3. **Cleaner Code**: No if-else statements for null handling
4. **No NullReferenceException**: Impossible to get null reference errors
5. **Consistent Interface**: All objects implement the same interface

## Example Output

**Customer: Lord Voldomort (None category)**
- Order: 5 × $1,500 = $7,500
- Discount: 0% (NullDiscountStrategy)
- Final Price: $7,500

**Customer: Alice (Gold category)**
- Order: 10 × $1,500 = $15,000
- Discount: 10% (GoldCustomerDiscountStrategy)
- Final Price: $13,500

This pattern makes your code safer and cleaner by eliminating the need for null checks! 