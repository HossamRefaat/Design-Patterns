# State Design Pattern

## What is the State Design Pattern?

The **State Design Pattern** is a behavioral pattern that allows an object to change its behavior when its internal state changes. The object appears to change its class. Think of it like a traffic light - it behaves differently (allows or stops traffic) based on its current state (red, yellow, or green), and each state determines what the next valid state can be.

## Key Benefits

- **Eliminates complex conditional logic** - No need for large if/else or switch statements
- **Encapsulates state-specific behavior** - Each state handles its own logic
- **Easy to add new states** - Simply create new state classes without modifying existing code
- **Clear state transitions** - Each state knows its valid next states
- **Single Responsibility** - Each state class has one clear purpose
- **Open/Closed Principle** - Open for extension (new states), closed for modification

## Key Components

1. **Context** (`Order`) - The object whose behavior changes based on its state
2. **State Interface** (`IOrderState`) - Defines the contract that all states must implement
3. **Concrete States** (`NewOrder`, `ProcessingOrder`, `ShippedOrder`, etc.) - Implement state-specific behavior
4. **Client** (`Program`) - Uses the context without knowing about specific states

## Our Implementation: Order Processing System

This project demonstrates the State pattern through an e-commerce order processing workflow where orders transition through different states with specific rules and behaviors.

### Order State Flow
```
New → Processing → Shipped → Delivered
  ↓       ↓
Cancelled  Cancelled
```

### 1. State Interface (`IOrderState.cs`)
```csharp
public interface IOrderState
{
    void Next(Order order);
    void Cancel(Order order);
    string GetStatus();
}
```
This defines the operations that all order states must support.

### 2. Context Class (`Order.cs`)
```csharp
public class Order
{
    private IOrderState _state;

    public Order()
    {
        _state = new NewOrder(); // Start with new order state
    }

    public void SetState(IOrderState state)
    {
        _state = state;
    }

    public void NextStep() => _state.Next(this);
    public void CancelOrder() => _state.Cancel(this);
    public string GetStatus() => _state.GetStatus();
}
```
The Order class delegates all state-dependent behavior to its current state object.

### 3. Concrete State Implementations

#### New Order State
```csharp
internal class NewOrder : IOrderState
{
    public void Next(Order order)
    {
        Console.WriteLine("Order moved to Processing.");
        order.SetState(new ProcessingOrder());
    }

    public void Cancel(Order order)
    {
        Console.WriteLine("Order cancelled.");
        order.SetState(new CancelledOrder());
    }

    public string GetStatus() => "New";
}
```
**Rules**: Can move to Processing or be Cancelled.

#### Processing Order State
```csharp
public class ProcessingOrder : IOrderState
{
    public void Next(Order order)
    {
        Console.WriteLine("Order shipped.");
        order.SetState(new ShippedOrder());
    }

    public void Cancel(Order order)
    {
        Console.WriteLine("Order cancelled.");
        order.SetState(new CancelledOrder());
    }

    public string GetStatus() => "Processing";
}
```
**Rules**: Can be shipped or cancelled (last chance to cancel).

#### Shipped Order State
```csharp
public class ShippedOrder : IOrderState
{
    public void Next(Order order)
    {
        Console.WriteLine("Order delivered.");
        order.SetState(new DeliveredOrder());
    }

    public void Cancel(Order order)
    {
        Console.WriteLine("Cannot cancel a shipped order.");
        // No state change - cancellation not allowed
    }

    public string GetStatus() => "Shipped";
}
```
**Rules**: Can only be delivered, cancellation is no longer allowed.

#### Delivered Order State
```csharp
public class DeliveredOrder : IOrderState
{
    public void Next(Order order)
    {
        Console.WriteLine("Order already delivered.");
        // No state change - final state
    }

    public void Cancel(Order order)
    {
        Console.WriteLine("Cannot cancel. Already delivered.");
        // No state change - cancellation not allowed
    }

    public string GetStatus() => "Delivered";
}
```
**Rules**: Final state - no further transitions allowed.

#### Cancelled Order State
```csharp
public class CancelledOrder : IOrderState
{
    public void Next(Order order)
    {
        Console.WriteLine("Cannot move forward. Order is cancelled.");
        // No state change - terminal state
    }

    public void Cancel(Order order)
    {
        Console.WriteLine("Already cancelled.");
        // No state change - already cancelled
    }

    public string GetStatus() => "Cancelled";
}
```
**Rules**: Terminal state - no transitions allowed.

### 4. Usage Example (`Program.cs`)
```csharp
var order = new Order();

Console.WriteLine("Status: " + order.GetStatus());  // "New"

order.NextStep(); // New -> Processing
Console.WriteLine("Status: " + order.GetStatus());  // "Processing"

order.NextStep(); // Processing -> Shipped  
Console.WriteLine("Status: " + order.GetStatus());  // "Shipped"

order.CancelOrder(); // Attempt to cancel (fails)
// Output: "Cannot cancel a shipped order."

order.NextStep(); // Shipped -> Delivered
Console.WriteLine("Status: " + order.GetStatus());  // "Delivered"
```
## Benefits in Our Implementation

1. **Clear Business Rules**: Each state encapsulates its own transition logic
2. **Type Safety**: Impossible to reach invalid states through proper encapsulation
3. **Easy Maintenance**: Adding new states (e.g., "Returned") doesn't affect existing states
4. **Readable Code**: No complex conditional statements cluttering the Order class
5. **Testability**: Each state can be tested independently

## Real-World Examples

- **Order Processing**: New → Processing → Shipped → Delivered
- **Document Workflow**: Draft → Review → Approved → Published
- **ATM Machine**: Idle → Card Inserted → PIN Entered → Transaction
- **Media Player**: Stopped → Playing → Paused → Stopped
- **Connection States**: Disconnected → Connecting → Connected → Disconnected
- **Game Character**: Standing → Walking → Running → Jumping

## When to Use State Pattern

✅ **Use When:**
- Object behavior depends on its state and must change at runtime
- You have complex conditional statements based on object state
- State transitions follow specific rules or workflows
- You want to avoid large switch/if-else statements
- States have different behaviors for the same operations

❌ **Avoid When:**
- You have simple state logic that doesn't change
- States don't affect object behavior significantly
- You only have a few states with simple transitions
- Performance is critical (state objects add overhead)

## Alternative Approaches

1. **Switch/If-Else Statements**: Simple but becomes complex with many states
2. **Enum with Switch**: Better organization but still requires central logic
3. **State Machine Libraries**: For very complex state machines
4. **Finite State Machine (FSM)**: More formal mathematical approach

## Summary

The State Pattern is like having **different personalities** for your object based on its current situation:

- **Each state** is a separate class with its own behavior
- **The context** delegates work to its current state
- **State transitions** are controlled by the states themselves
- **Business rules** are encapsulated within each state
- **No complex conditionals** needed in the main class

In our order processing example, the Order class doesn't need to know about all the complex rules for when orders can be cancelled, shipped, or delivered. Each state handles its own logic, making the code cleaner, more maintainable, and easier to extend with new states like "Returned" or "Refunded"! 