# Strategy Pattern: Customer Discount System

## What is the Strategy Pattern?

Think of the Strategy pattern like having different **tools for the same job**. Instead of having one big complicated method that tries to handle everything, you create separate, simple tools (strategies) and pick the right one when you need it.

**Simple Example:** Imagine you're an **online store** that offers different **customer loyalty levels**:
- New customers (0% discount)
- Silver customers (5% discount on orders over $10,000)
- Gold customers (10% discount on orders over $10,000)

Each loyalty level is a different "strategy" - they all do the same job (calculate discount) but give different amounts off based on customer type and order size.

## What This Project Does

This project simulates an **online store** that gives different **discounts** to different types of customers:

- **New Customers**: No discount (0%)
- **Silver Customers**: 5% discount on orders over $10,000
- **Gold Customers**: 10% discount on orders over $10,000

## How It Works

### Step 1: Customer Types
We have 3 customers in our system:
- **Alice** (Gold customer)
- **Bob** (Silver customer) 
- **Charlie** (New customer)

### Step 2: When You Run the Program
1. You see a list of customers
2. You pick a customer by entering their ID (1, 2, or 3)
3. You enter the quantity and price of items
4. The system calculates the discount based on the customer type
5. You see the final price with the discount applied

### Step 3: Example Results

**Scenario:** Order 10 items at $1,500 each = $15,000 total

| Customer | Customer Type | Discount | Final Price |
|----------|---------------|----------|-------------|
| Alice    | Gold          | 10%      | $13,500     |
| Bob      | Silver        | 5%       | $14,250     |
| Charlie  | New           | 0%       | $15,000     |

## How the Strategy Pattern is Used Here

### The Problem Without Strategy Pattern
Without the Strategy pattern, you'd have code like this:
```csharp
// BAD: Lots of if-else statements
if (customer.Category == CustomerCategory.Gold)
{
    if (totalPrice >= 10000)
        discount = 0.10;
    else
        discount = 0;
}
else if (customer.Category == CustomerCategory.Silver)
{
    if (totalPrice >= 10000)
        discount = 0.05;
    else
        discount = 0;
}
else
{
    discount = 0;
}
```

### The Solution With Strategy Pattern
We create separate classes for each discount type:

```csharp
// Gold customers get 10% discount on orders over $10,000
public class GoldCustomerDiscountStrategy
{
    public double CalculateDiscount(double totalPrice)
    {
        return totalPrice >= 10000 ? 0.10 : 0;
    }
}

// Silver customers get 5% discount on orders over $10,000  
public class SilverCustomerDiscountStrategy
{
    public double CalculateDiscount(double totalPrice)
    {
        return totalPrice >= 10000 ? 0.05 : 0;
    }
}

// New customers get no discount
public class NewCustomerDiscountStrategy
{
    public double CalculateDiscount(double totalPrice)
    {
        return 0;
    }
}
```

### How We Pick the Right Strategy
```csharp
// Pick the right discount strategy based on customer type
ICustomerDiscountStrategy strategy;
if (customer.Category == CustomerCategory.Gold)
    strategy = new GoldCustomerDiscountStrategy();
else if (customer.Category == CustomerCategory.Silver)
    strategy = new SilverCustomerDiscountStrategy();
else
    strategy = new NewCustomerDiscountStrategy();

// Use the strategy to calculate discount
double discount = strategy.CalculateDiscount(totalPrice);
```

### The CreateInvoice Method Explained

The `CreateInvoice` method in `InvoiceManager` is where the magic happens:

```csharp
public Invoice CreateInvoice(Customer customer, int quantity, int unitPrice)
{
    // 1. Create a new invoice with customer and line items
    var invoice = new Invoice
    {
        Customer = customer,
        Lines = new List<InvoiceLine>
        {
            new InvoiceLine
            {
                Quantity = quantity,
                UnitPrice = unitPrice
            }
        },
    };
    
    // 2. Calculate the discount using the selected strategy
    invoice.DiscountPercentage = _customerDiscountStrategy.CalculateDiscount(invoice.TotalPrice);
    
    // 3. Return the invoice with discount applied
    return invoice;
}
```

**What this method does:**
1. **Creates the invoice** with customer info and line items
2. **Calculates total price** (quantity × unit price)
3. **Applies the discount** using the selected strategy
4. **Returns the final invoice** with net price calculated

**The key line:** `invoice.DiscountPercentage = _customerDiscountStrategy.CalculateDiscount(invoice.TotalPrice);`

This is where the Strategy pattern shines - the method doesn't know which discount strategy it's using, it just calls the `CalculateDiscount` method through the interface and gets the right discount amount! This is **polymorphism** in action - the same method call behaves differently based on which strategy object is being used.

## Why This is Better

### ✅ Advantages
1. **Easy to Add New Customer Types**: Just create a new strategy class
2. **Easy to Change Discount Rules**: Modify one strategy without affecting others
3. **Clean Code**: No messy if-else statements
4. **Testable**: Each strategy can be tested separately
5. **Flexible**: Can change strategies at runtime

### ❌ Without Strategy Pattern
- Hard to add new customer types
- Hard to modify discount rules
- Lots of if-else statements
- Difficult to test
- Code becomes messy and hard to maintain

## Project Files Explained

| File | What It Does |
|------|-------------|
| `Program.cs` | Main program - handles user input and shows results |
| `Customer.cs` | Customer information (name, ID, type) |
| `Invoice.cs` | Invoice with total price and final price after discount |
| `InvoiceManager.cs` | Uses the discount strategy to calculate final price |
| `DiscountStrategies/` | Folder containing all discount calculation methods |
| `ICustomerDiscountStrategy.cs` | Interface that all discount strategies must follow |
| `GoldCustomerDiscountStrategy.cs` | 10% discount for gold customers |
| `SilverCustomerDiscountStrategy.cs` | 5% discount for silver customers |
| `NewCustomerDiscountStrategy.cs` | No discount for new customers |

## When to Use Strategy Pattern

Use the Strategy pattern when:
- You have different ways to do the same thing
- You want to avoid lots of if-else statements
- You want to easily add new ways of doing something
- You want to change behavior at runtime

**Real-world examples:**
- Payment methods (cash, credit card, PayPal)
- Sorting algorithms (bubble sort, quick sort, merge sort)
- Compression algorithms (ZIP, RAR, 7Z)
- Discount calculations (like in this project!)

This pattern makes your code more organized, flexible, and easier to maintain! 