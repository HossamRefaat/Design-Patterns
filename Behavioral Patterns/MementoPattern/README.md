# Memento Pattern Implementation

## Overview

This project demonstrates the **Memento Pattern** implementation in C#, combined with the **Command Pattern** to create a comprehensive order management system with state saving/restoration and undo/redo functionality.

## What is the Memento Pattern?

The Memento Pattern is a behavioral design pattern that allows you to save and restore the previous state of an object without revealing the details of its implementation. It provides the ability to capture an object's internal state and restore it later, enabling features like undo/redo, checkpoints, and state history.

### Key Components

1. **Originator (`Order`)**: The object whose state needs to be saved and restored
2. **Memento (`OrderMemento`)**: A snapshot of the originator's state
3. **Caretaker (`Caretaker`)**: Manages and stores mementos without accessing their content

## Architecture

```
┌─────────────────┐    creates    ┌──────────────────┐
│     Order       │─────────────▶│   OrderMemento   │
│  (Originator)   │               │    (Memento)     │
└─────────────────┘               └──────────────────┘
         │                                  │
         │                                  │
         ▼                                  ▼
┌─────────────────┐    manages    ┌──────────────────┐
│   Caretaker     │◄──────────────│  List<Memento>   │
│                 │               │                  │
└─────────────────┘               └──────────────────┘
```

## Features

### Core Memento Pattern Features
- **State Saving**: Save order state at any point in time
- **State Restoration**: Restore order to a previously saved state
- **Multiple Snapshots**: Store multiple mementos with indexed access

### Additional Command Pattern Integration
- **Command Execution**: Add products using command objects
- **Undo/Redo**: Reverse and replay commands
- **Macro Recording**: Save command sequences for later replay
- **Stock Management**: Automatic stock updates with commands

## Project Structure

```
MementoPattern/
├── Core/
│   ├── Memento/
│   │   ├── OrderMemento.cs      # Memento - stores order state
│   │   └── Caretaker.cs         # Caretaker - manages mementos
│   ├── Commands/
│   │   ├── ICommand.cs          # Command interface
│   │   ├── AddProductCommand.cs # Command to add products
│   │   ├── AddStockCommand.cs   # Command to manage stock
│   │   ├── CommandInvoker.cs    # Command execution manager
│   │   ├── Macro.cs             # Command sequence container
│   │   └── MacroStorage.cs      # Macro management
│   ├── Order.cs                 # Originator - main business object
│   ├── OrderLine.cs             # Order line item
│   └── Product.cs               # Product entity
└── Program.cs                   # Demo application
```

## Key Classes

### OrderMemento
The memento class that stores a snapshot of the order's state:

```csharp
internal class OrderMemento
{
    private readonly IEnumerable<OrderLine> lines;
    
    public OrderMemento(IEnumerable<OrderLine> lines)
    {
        this.lines = lines;
    }
    
    public IEnumerable<OrderLine> GetLines() => lines;
}
```

### Order (Originator)
The originator that can save and restore its state:

```csharp
public OrderMemento SaveStateToMemento()
{
    return new OrderMemento(_lines.ToArray());
}

public void RestoreStateFromMemento(OrderMemento memento)
{
    if (memento == null)
    {
        throw new ArgumentNullException(nameof(memento), "Cannot restore state from a null memento.");
    }
    _lines = memento.GetLines().ToList();
}
```

### Caretaker
The caretaker that manages multiple mementos:

```csharp
internal class Caretaker
{
    private List<OrderMemento> _mementos = new();
    
    public int AddMemento(OrderMemento memento)
    {
        _mementos.Add(memento);
        return _mementos.Count - 1;
    }
    
    public OrderMemento GetMemento(int index)
    {
        if (index < 0 || index >= _mementos.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Invalid memento index.");
        }
        return _mementos[index];
    }
}
```

## How to Use

### Prerequisites
- .NET 6.0 or later
- Visual Studio 2022 or VS Code with C# extension

### Running the Application

1. **Navigate to the project directory:**
   ```bash
   cd "Behavioral Patterns/MementoPattern/MementoPattern"
   ```

2. **Build and run:**
   ```bash
   dotnet build
   dotnet run
   ```

### Available Commands

The application provides an interactive menu with the following options:

1. **Add Laptop** - Adds a laptop to the order
2. **Add Keyboard** - Adds a keyboard to the order  
3. **Add Mouse** - Adds a mouse to the order
4. **Save Macro** - Saves current command sequence as a macro
5. **Replay Macro** - Replays a previously saved macro
6. **Undo** - Undoes the last two commands
7. **Redo** - Redoes the last two undone commands
8. **Save Memento** - Saves current order state to memento
9. **Restore Memento** - Restores order from a saved memento
0. **Process** - Processes the current order

### Example Workflow

1. **Add some products** to your order (options 1-3)
2. **Save a memento** (option 8) to capture the current state
3. **Add more products** to modify the order
4. **Restore from memento** (option 9) to return to the saved state
5. **Use undo/redo** (options 6-7) for command-level reversals

## Key Differences: Memento vs. Macro Recording

### Memento Recording (State-Based)
- **What it records**: Complete snapshots of object state
- **How it works**: Captures entire internal state at a moment in time
- **Restoration**: Overwrites current state with saved snapshot
- **Use case**: "Save game" functionality, checkpoints

### Macro Recording (Behavior-Based)  
- **What it records**: Sequence of commands/operations
- **How it works**: Records individual actions that were performed
- **Restoration**: Re-executes the same operations
- **Use case**: Automation, batch operations, templates

## Design Benefits

### Memento Pattern Benefits
- **Encapsulation**: Object's internal state remains private
- **Simplicity**: Clean interface for save/restore operations  
- **Flexibility**: Multiple snapshots can be maintained
- **Undo Support**: Easy implementation of undo functionality

### Combined with Command Pattern
- **Granular Control**: Command-level undo vs. state-level restore
- **Macro Support**: Record and replay command sequences
- **Audit Trail**: Track both commands and state changes
- **Flexibility**: Choose between command undo or state restoration

## When to Use Memento Pattern

✅ **Good for:**
- Implementing undo/redo functionality
- Creating checkpoints in applications
- Rolling back to previous states
- Implementing save/load game states
- Transaction rollback scenarios

❌ **Avoid when:**
- Object state is simple and doesn't change frequently
- Memory usage is a critical concern (mementos consume memory)
- The cost of creating snapshots is too high
- Simple property-based undo is sufficient

## Learning Outcomes

After studying this implementation, you will understand:

1. **Memento Pattern Structure**: How Originator, Memento, and Caretaker work together
2. **State Management**: Different approaches to saving and restoring object state
3. **Pattern Combination**: How Memento and Command patterns complement each other
4. **Encapsulation**: How to maintain object privacy while enabling state access
5. **Undo Mechanisms**: Different levels of undo (command vs. state-based)

## Extending the Code

Consider these enhancements:

- **Named Snapshots**: Add descriptions to mementos
- **Compression**: Compress large state objects
- **Persistence**: Save mementos to disk
- **Expiration**: Auto-cleanup old mementos
- **Differential Snapshots**: Store only changes between states

## Related Patterns

- **Command Pattern**: Used for undo/redo at the operation level
- **Observer Pattern**: Could notify about state changes
- **Strategy Pattern**: Different memento storage strategies
- **Prototype Pattern**: Alternative approach to object copying

## Testing the Implementation

The project includes comprehensive testing scenarios:

1. **Basic Operations**: Add products and verify order state
2. **Memento Save/Restore**: Test state snapshots and restoration
3. **Command Undo/Redo**: Test command-level reversals
4. **Macro Recording**: Test command sequence recording and replay
5. **Edge Cases**: Test with empty orders, invalid indices, etc.

---

*This implementation demonstrates both the theoretical concepts and practical applications of the Memento pattern in a real-world scenario.*