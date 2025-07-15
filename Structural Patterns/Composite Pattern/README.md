# Composite Pattern Implementation in C#

## Overview

This project demonstrates the implementation of the **Composite Pattern**, one of the structural design patterns. The composite pattern allows you to compose objects into tree structures to represent part-whole hierarchies, enabling clients to treat individual objects and compositions of objects uniformly.

## What is the Composite Pattern?

The Composite Pattern is a structural design pattern that lets you compose objects into tree structures and work with these structures as if they were individual objects. It's particularly useful when you need to implement a tree-like object structure.

### Key Components

1. **Component**: An interface or abstract class that defines the common operations for both simple and complex objects
2. **Leaf**: A basic element that doesn't have sub-elements
3. **Composite**: An element that has sub-elements (leaves or other composites)

### Benefits

- **Uniformity**: Clients can treat individual objects and compositions uniformly
- **Flexibility**: Easy to add new types of components
- **Simplicity**: Simplifies client code by eliminating the need to distinguish between leaf and composite objects

## Implementation in This Project

This project implements the composite pattern using a building/housing hierarchy:

### 1. Component Interface (`IStructure`)

```csharp
public interface IStructure
{
    void Enter();
    void Exit();
    void Location();
    string GetName();
}
```

The `IStructure` interface defines the common operations that both leaf (rooms) and composite (housing) objects must implement.

### 2. Composite Class (`Housing`)

```csharp
public class Housing : IStructure
{
    private readonly List<IStructure> structures;
    private readonly string address;
    
    // Methods to manage child structures
    public int AddStructure(IStructure structure)
    public IStructure GetStructure(int index)
    
    // Implementation of IStructure methods
    public void Enter() { /* Implementation */ }
    public void Exit() { /* Implementation */ }
    public void Location() { /* Implementation */ }
    public string GetName() { /* Implementation */ }
}
```

The `Housing` class acts as the composite that can contain other structures (rooms or other housing units like floors).

### 3. Leaf Class (`Room`)

```csharp
public class Room : IStructure
{
    private readonly string name;
    
    // Implementation of IStructure methods
    public void Enter() { /* Implementation */ }
    public void Exit() { /* Implementation */ }
    public void Location() { /* Implementation */ }
    public string GetName() { /* Implementation */ }
}
```

The `Room` class represents the leaf objects that don't contain other structures.

## Usage Example

The `Program.cs` demonstrates how the composite pattern works:

```csharp
// Create main house (composite)
Housing myHouse = new Housing("123 Main St");

// Create first floor (composite)
Housing floor1 = new Housing("123 Main St - First Floor");

// Add floor to house
int firstFloor = myHouse.AddStructure(floor1);

// Create rooms (leaves)
Room washRoom1m = new Room("1F Men's Washroom");
Room washRoom1w = new Room("1F Women's Washroom");
Room common1 = new Room("Common Area");

// Add rooms to floor
floor1.AddStructure(washRoom1m);
floor1.AddStructure(washRoom1w);
floor1.AddStructure(common1);

// Navigate through the structure
myHouse.Enter();                    // Enter house
currentFloor.Enter();               // Enter floor
currentRoom.Enter();                // Enter room
```
## Real-World Applications

The composite pattern is useful in scenarios such as:
- File system structures (folders and files)
- GUI component hierarchies (windows, panels, buttons)
- Organization structures (departments, teams, employees)
- Menu systems (menus, submenus, menu items)
- Building/architectural layouts (as demonstrated in this project)

## Advantages Demonstrated

1. **Simplified Client Code**: The client doesn't need to distinguish between individual rooms and complex housing structures
2. **Easy Extension**: New structure types can be added by implementing the `IStructure` interface
3. **Recursive Operations**: Operations can be performed on the entire structure uniformly
4. **Flexible Composition**: Structures can be composed in various ways to create different building layouts 