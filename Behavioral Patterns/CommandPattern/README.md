# Command Design Pattern

## What is the Command Design Pattern?

The **Command Design Pattern** is a behavioral pattern that encapsulates a request as an object, allowing you to parameterize clients with different requests, queue operations, and support undo functionality. Think of it like a remote control - each button is a command object that knows exactly what action to perform when pressed, and you can even record sequences of button presses to replay later.

## Key Benefits

- **Decouples sender from receiver** - The invoker doesn't need to know how operations are performed
- **Undo/Redo functionality** - Commands can reverse their actions
- **Macro recording** - Chain commands together for complex operations
- **Queuing and scheduling** - Commands can be stored and executed later
- **Logging and auditing** - Track all operations performed
- **Request parameterization** - Same interface for different operations

## Key Components

1. **Command Interface** (`ICommand`) - Defines the contract for all commands
2. **Concrete Commands** (`AddProductCommand`, `AddStockCommand`) - Implement specific operations
3. **Invoker** (`CommandInvoker`) - Manages command execution and history
4. **Receivers** (`Order`, `Product`) - Objects that actually perform the work
5. **Client** (`Program`) - Creates commands and configures the invoker

## Our Implementation: Order Processing System

This project demonstrates the Command pattern through an interactive order processing system where users can build orders, manage inventory, and utilize advanced features like undo/redo and macro recording.

### Core Features
- **Product Management**: Add products to orders with automatic stock updates
- **Undo/Redo**: Reverse and replay actions
- **Macro Recording**: Save and replay command sequences
- **Batch Processing**: Execute multiple commands together

### 1. Command Interface (`ICommand.cs`)
```csharp
internal interface ICommand
{
    void Execute();
    void Undo();
}
```
This defines the contract that all commands must implement - both execution and reversal.

### 2. Concrete Commands

#### Add Product Command
```csharp
internal class AddProductCommand : ICommand
{
    public Order Order { get; set; }
    private readonly Product product;
    private readonly double quantity;

    public AddProductCommand(Order order, Product product, double quantity)
    {
        this.Order = order;
        this.product = product;
        this.quantity = quantity;
    }

    public void Execute()
    {
        Order.AddProduct(product, quantity);
    }

    public void Undo()
    {
        Order.RemoveProductAt(Order.Lines.Count() - 1);
    }
}
```
**Purpose**: Encapsulates adding a product to an order with undo capability.

#### Add Stock Command
```csharp
internal class AddStockCommand : ICommand
{
    private readonly Product product;
    private readonly double quantity;

    public AddStockCommand(Product product, double quantity)
    {
        this.product = product;
        this.quantity = quantity;
    }

    public void Execute()
    {
        product.AddStock(quantity);
    }

    public void Undo()
    {
        product.AddStock(quantity * -1); // Reverse the stock change
    }
}
```
**Purpose**: Manages inventory changes with automatic reversal capability.

### 3. Command Invoker (`CommandInvoker.cs`)
```csharp
internal class CommandInvoker
{
    private List<ICommand> commands = new();
    private Stack<ICommand> executedCommands = new();   
    private Stack<ICommand> undoneCommands = new();   

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        executedCommands.Push(command);
    }

    public void Undo()
    {
        var command = executedCommands.Pop();
        command.Undo();
        undoneCommands.Push(command);
    }

    public void Redo()
    {
        if (undoneCommands.Count > 0)
        {
            var command = undoneCommands.Pop();
            ExecuteCommand(command);
        }
    }

    public void ExecuteCommands()
    {
        foreach (var command in commands)
        {
            ExecuteCommand(command);
        }
        ClearCommand(); 
    }
}
```
**Features**: 
- **Command History**: Tracks executed commands for undo/redo
- **Batch Execution**: Execute multiple commands together
- **State Management**: Maintains separate stacks for undo and redo operations

### 4. Receivers

#### Order Class (`Order.cs`)
```csharp
internal class Order
{
    public int Id { get; } = Random.Shared.Next(1, 1000);
    private List<OrderLine> _lines = new();
    public IEnumerable<OrderLine> Lines => _lines.AsReadOnly();

    public void AddProduct(Product product, double quantity)
    {
        _lines.Add(new OrderLine { 
            ProductId = product.Id, 
            UnitPrice = product.UnitPrice, 
            Quantity = quantity 
        });
        Console.WriteLine($"Product `{product.Name}` added, Order now contains {_lines.Count} products");
    }

    internal void RemoveProductAt(int index)
    {
        _lines.RemoveAt(index);
    }
}
```

#### Product Class (`Product.cs`)
```csharp
internal class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public double UnitPrice { get; private set; }
    public double StockBalance { get; private set; }

    public void AddStock(double quantity)
    {
        StockBalance += quantity;
        Console.WriteLine($"Product `{Name}` stock changed to {StockBalance}");
    }
}
```

### 5. Macro System

#### Macro Class (`Macro.cs`)
```csharp
internal class Macro
{
    public int Id { get; }
    public IEnumerable<ICommand> Commands { get; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
```

#### Macro Storage (`MacroStorage.cs`)
```csharp
internal class MacroStorage
{
    public static MacroStorage Instance { get; } = new();
    private List<Macro> _macros = new();

    public void CreateMacro(IEnumerable<ICommand> commands)
    {
        var macro = new Macro(_macros.Count + 1, commands.ToList());
        _macros.Add(macro);
        Console.WriteLine($"Macro #{macro.Id} saved.");
    }

    public Macro GetMacro(int id) => _macros.First(x => x.Id == id);
    public IEnumerable<Macro> GetMacros() => _macros.AsReadOnly();
}
```

### 6. Usage Example (`Program.cs`)
```csharp
var laptop = new Product(1, "Laptop", 20000, 10);
var keyboard = new Product(2, "Keyboard", 300, 50);
var mouse = new Product(3, "Mouse", 150, 70);

var order = new Order();
var invoker = new CommandInvoker();

// Add products to order (with automatic stock management)
invoker.ExecuteCommand(new AddProductCommand(order, laptop, 1));
invoker.ExecuteCommand(new AddStockCommand(laptop, -1));

// Undo the last operations
invoker.Undo(); // Undo stock change
invoker.Undo(); // Undo product addition

// Redo the operations
invoker.Redo(); // Redo product addition
invoker.Redo(); // Redo stock change

// Save current commands as a macro
MacroStorage.Instance.CreateMacro(invoker.GetCommands());

// Replay a saved macro
var savedMacro = MacroStorage.Instance.GetMacro(1);
foreach(var command in savedMacro.Commands)
{
    invoker.ExecuteCommand(command);
}
```

## Interactive Menu System

The application provides an interactive menu with the following options:

```
1. Add Laptop      - Adds laptop to order and decreases stock
2. Add Keyboard    - Adds keyboard to order and decreases stock  
3. Add Mouse       - Adds mouse to order and decreases stock
4. Save Macro      - Records current command sequence
5. Replay Macro    - Executes a previously saved macro
6. Undo           - Reverses the last two operations
7. Redo           - Replays the last undone operations
0. Process        - Finalizes and displays order summary
```
## Benefits in Our Implementation

1. **Flexible Operations**: Easy to add new product types or commands
2. **Complete Reversibility**: Every action can be undone and redone
3. **Macro Functionality**: Record complex workflows for later replay
4. **Separation of Concerns**: UI logic separate from business operations
5. **Extensibility**: New commands can be added without changing existing code

## Real-World Examples

- **Text Editors**: Cut, Copy, Paste, Undo, Redo operations
- **Database Transactions**: Execute, Rollback, Commit operations
- **GUI Applications**: Button clicks, menu selections
- **Task Schedulers**: Queue jobs for later execution
- **Game Development**: Player actions, replay systems

## Summary

The Command Pattern transforms **actions into objects**, providing incredible flexibility:

- **Each command** is a self-contained object that knows how to execute and undo itself
- **The invoker** manages execution flow without knowing command details
- **Receivers** focus only on the actual business logic
- **Macros** enable complex workflow automation
- **Undo/Redo** provides safety and user-friendly experience

In our e-commerce example, users can build orders interactively, experiment freely with undo/redo, and create reusable macros for common purchasing patterns. The pattern makes the system both powerful and user-friendly while maintaining clean, maintainable code! 