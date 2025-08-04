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

## Design Benefits

### Memento Pattern Benefits
- **Encapsulation**: Object's internal state remains private
- **Simplicity**: Clean interface for save/restore operations  
- **Flexibility**: Multiple snapshots can be maintained
- **Undo Support**: Easy implementation of undo functionality
- 
## When to Use Memento Pattern

✅ **Good for:**
- Implementing undo/redo functionality
- Creating checkpoints in applications
- Rolling back to previous states
- Implementing save/load game states
- Transaction rollback scenarios

---

*This implementation demonstrates both the theoretical concepts and practical applications of the Memento pattern in a real-world scenario.*
