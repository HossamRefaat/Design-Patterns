# Simple Factory Pattern: Customer Discount Strategy Factory

## What is the Simple Factory Pattern?

The Simple Factory pattern is a **creational design pattern** that provides a centralized way to create objects without exposing the instantiation logic to the client. Think of it as a **"smart object creator"** that knows how to create the right type of object based on your requirements.

## What This Project Does

This project demonstrates the **Simple Factory pattern** using the same customer discount system as the [Strategy Pattern](../Behavioral%20Patterns/StrategyPattern/README.md). The key difference is that instead of manually creating discount strategies, we use a **factory** to create them for us.

## How the Simple Factory Pattern is Used Here

### The Problem Without Factory
In the original Strategy pattern, we had to manually create strategies:
```csharp
// Manual creation - lots of if-else statements
ICustomerDiscountStrategy strategy;
if (customer.Category == CustomerCategory.Gold)
    strategy = new GoldCustomerDiscountStrategy();
else if (customer.Category == CustomerCategory.Silver)
    strategy = new SilverCustomerDiscountStrategy();
else
    strategy = new NewCustomerDiscountStrategy();
```

### The Solution With Simple Factory
We create a **factory** that handles the object creation:
```csharp
// Clean factory usage
var factory = new CustomerDiscountStrategyFactory();
ICustomerDiscountStrategy strategy = factory.CreateCustomerDiscountStrategy(customer.Category);
```

### The Factory Implementation
```csharp
public class CustomerDiscountStrategyFactory
{
    public ICustomerDiscountStrategy CreateCustomerDiscountStrategy(CustomerCategory category)
    {
        if (category == CustomerCategory.Silver)
        {
            return new SilverCustomerDiscountStrategy();
        }
        else if (category == CustomerCategory.Gold)
        {
            return new GoldCustomerDiscountStrategy();
        }
        else
        {
            return new NewCustomerDiscountStrategy();
        }
    }
}
```

## Why This is Better

### ✅ Advantages of Simple Factory
1. **Centralized Creation**: All object creation logic is in one place
2. **Easy to Modify**: Change creation logic in one place
3. **Better Encapsulation**: Client doesn't need to know about concrete classes
4. **Reusable**: Factory can be used anywhere in the application

### ❌ Without Factory Pattern
- Object creation scattered throughout the code
- Lots of if-else statements everywhere
- Hard to modify creation logic
- Client code knows about concrete classes
- Code duplication

## When to Use Simple Factory Pattern

Use the Simple Factory pattern when:
- You have a family of related objects to create
- You want to centralize object creation logic
- You want to hide object creation complexity from clients
- You want to make object creation more maintainable

## Full Project Explanation

For a complete explanation of the customer discount system, business logic, and how the strategies work, see the **[Strategy Pattern README](../Behavioral%20Patterns/StrategyPattern/README.md)**.

This Simple Factory pattern makes the object creation process cleaner and more maintainable while keeping all the same business functionality! 