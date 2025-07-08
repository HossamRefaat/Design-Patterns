# Factory Method Pattern

## What is the Factory Method Pattern?

The **Factory Method Pattern** is a creational design pattern that provides an interface for creating objects in a superclass, but allows subclasses to alter the type of objects that will be created. Instead of calling a constructor directly, you call a factory method to create objects.

### The Pattern Structure:
1. **Abstract Factory**: Defines the method to create objects
2. **Concrete Factory**: Implements the creation method for specific objects
3. **Product Interface**: Common interface for all created objects
4. **Concrete Products**: Specific implementations of the interface

## How This Project Works

This project demonstrates the Factory Method Pattern through a **Payment Processing System** integrated with an e-commerce shopping cart application.

### Project Components:

#### 1. **Payment Abstractions** (`FactoryMethodPattern.Payments.Abstractions`)
- `IPaymentMethod`: Interface defining payment behavior
- `PaymentProcessor`: Abstract creator class with factory method
- `Payment`: Data class representing payment information

#### 2. **Concrete Payment Implementations**
- `VisaPaymentProcessor`: Creates Visa payment methods
- `PayPalPaymentProcessor`: Creates PayPal payment methods
- `VisaPaymentMethod`: Handles Visa-specific payment logic
- `PayPalPaymentMethod`: Handles PayPal-specific payment logic

#### 3. **Shopping Cart System**
- `ShoppingCart`: Abstract base class for cart functionality
- `OnlineShoppingCart`: Applies online-specific discounts
- `InStoreShoppingCart`: No discount application
- `Invoice`: Calculates totals and applies discounts

### Application Flow:
1. User selects customer and shopping cart type
2. User adds items to cart
3. User chooses payment method (Visa or PayPal)
4. Factory Method creates appropriate payment processor
5. Payment is processed with method-specific fees

## Factory Method Implementation

### 1. Abstract Creator (`PaymentProcessor`)
```csharp
public abstract class PaymentProcessor
{
    public Payment ProcessPayment(int customerId, double amount)
    {
        var paymentMethod = CreatePaymentMethod(); // Factory Method
        var payment = paymentMethod.Charge(customerId, amount);
        return payment;
    }

    protected abstract IPaymentMethod CreatePaymentMethod(); // Factory Method
}
```

### 2. Concrete Creators
```csharp
public class VisaPaymentProcessor : PaymentProcessor
{
    protected override IPaymentMethod CreatePaymentMethod()
    {
        return new VisaPaymentMethod(); // Creates Visa-specific implementation
    }
}

public class PayPalPaymentProcessor : PaymentProcessor
{
    protected override IPaymentMethod CreatePaymentMethod()
    {
        return new PayPalPaymentMethod(); // Creates PayPal-specific implementation
    }
}
```

### 3. Product Interface (`IPaymentMethod`)
```csharp
public interface IPaymentMethod
{
    Payment Charge(int customerId, double amount);
}
```

### 4. Concrete Products
```csharp
class VisaPaymentMethod : IPaymentMethod
{
    public Payment Charge(int customerId, double amount)
    {
        return new Payment
        {
            CustomerId = customerId,
            ChargedAmount = amount + (amount < 10000 ? amount * 0.02 : 0), // 2% fee if < 10,000
            ReferenceNumber = Guid.NewGuid()
        };
    }
}

class PayPalPaymentMethod : IPaymentMethod
{
    public Payment Charge(int customerId, double amount)
    {
        return new Payment
        {
            CustomerId = customerId,
            ChargedAmount = amount + amount * 0.05, // Always 5% fee
            ReferenceNumber = Guid.NewGuid()
        };
    }
}
```
