# Decorator Design Pattern

## What is the Decorator Design Pattern?

The **Decorator Design Pattern** is a structural pattern that allows you to dynamically add new behaviors to objects by wrapping them in special wrapper objects called decorators. Think of it like adding layers of functionality, similar to how you can add toppings to a pizza or accessories to an outfit - each layer adds something new without changing the core item.

## Key Benefits

- **Add functionality at runtime** without modifying existing code
- **Combine multiple behaviors** in different ways
- **Follow Open/Closed Principle** - open for extension, closed for modification
- **Alternative to inheritance** - more flexible than creating subclasses
- **Single Responsibility** - each decorator has one specific purpose

## Key Components

1. **Component Interface** (`IOrderProcessor`) - Defines operations that can be decorated
2. **Concrete Component** (`OrderProcessor`) - The base object that provides core functionality
3. **Decorator Classes** - Wrapper objects that add new behaviors while maintaining the same interface
4. **Client** (`Program`) - Uses decorated objects without knowing the decoration details

## Our Implementation: Order Processing System

This project demonstrates the decorator pattern through an order processing system where we can add different cross-cutting concerns like exception handling, performance profiling, and queuing.

### 1. Component Interface (`IOrderProcessor.cs`)
```csharp
internal interface IOrderProcessor
{
    void Process(Order order);
}
```
This defines the contract that both the base processor and all decorators must follow.

### 2. Concrete Component (`OrderProcessor.cs`)
```csharp
internal class OrderProcessor : IOrderProcessor
{
    public virtual void Process(Order order)
    {
        if(order.Lines.Count() == 0)
            throw new InvalidOperationException("Cannot process an order with no lines.");

        Thread.Sleep(Random.Shared.Next(1000, 3000)); // Simulate processing time
        Console.WriteLine("Order has been processed");
    }
}
```
This is the core implementation that actually processes orders.

### 3. Decorators

#### Exception Handling Decorator
```csharp
internal class OrderProcessorExceptionHandlingDecorator : IOrderProcessor
{
    private readonly IOrderProcessor orderProcessor;

    public OrderProcessorExceptionHandlingDecorator(IOrderProcessor orderProcessor)
    {
        this.orderProcessor = orderProcessor;
    }

    public void Process(Order order)
    {
        try
        {
            orderProcessor.Process(order);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while processing the order: {ex.Message}");
        }
    }
}
```
**Purpose**: Adds exception handling around order processing to gracefully handle errors.

#### Profiling Decorator
```csharp
internal class OrderProcessorProfilingDecorator : IOrderProcessor
{
    private readonly IOrderProcessor orderProcessor;

    public void Process(Order order)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        orderProcessor.Process(order);
        
        stopwatch.Stop();
        Console.WriteLine($"Order took `{stopwatch.Elapsed.TotalSeconds}s` to be processed");
    }
}
```
**Purpose**: Adds performance monitoring to measure how long order processing takes.

#### Queuing Decorator
```csharp
internal class OrderProcessorQueuingDecorator : IOrderProcessor
{
    private readonly IOrderProcessor orderProcessor;
    private Queue<Order> orders = new();

    public void Process(Order order)
    {
        orders.Enqueue(order);
        Console.WriteLine("Order has been queued.");
    }
}
```
**Purpose**: Adds queuing functionality to batch process orders later.

### 4. Usage Example (`Program.cs`)
```csharp
var order = new Order();
order.AddLine(1, 2, 10.00m);
order.AddLine(2, 1, 20.00m);
order.AddLine(3, 5, 5.00m);

// Start with base processor
IOrderProcessor orderProcessor = new OrderProcessor();

// Wrap with decorators (each adding new functionality)
orderProcessor = new OrderProcessorExceptionHandlingDecorator(orderProcessor);
orderProcessor = new OrderProcessorProfilingDecorator(orderProcessor);
orderProcessor = new OrderProcessorQueuingDecorator(orderProcessor);

// Process the order (all decorators will be applied)
orderProcessor.Process(order);
```
## Real-World Examples

- **Web API Middleware**: Authentication, logging, compression
- **Stream Processing**: Encryption, compression, buffering
- **UI Components**: Borders, scrollbars, shadows
- **Coffee Shop**: Espresso + Milk + Sugar + Whipped Cream
- **File Systems**: Compression + Encryption + Caching

## When to Use Decorator Pattern

✅ **Use When:**
- You need to add functionality to objects dynamically
- You want to add multiple independent features
- Inheritance would create too many subclass combinations
- You need to add cross-cutting concerns (logging, security, etc.)

❌ **Avoid When:**
- You only need simple, static functionality
- The decoration logic is very complex
- Performance is critical (decorators add method call overhead)

## Summary

The Decorator Pattern is like building with LEGO blocks - you start with a basic component and snap on additional pieces (decorators) to add new capabilities. Each decorator:

- **Wraps** another component
- **Adds** its own behavior  
- **Delegates** to the wrapped component
- **Maintains** the same interface

In our order processing example, we can dynamically combine exception handling, performance monitoring, and queuing without changing the core order processing logic! 