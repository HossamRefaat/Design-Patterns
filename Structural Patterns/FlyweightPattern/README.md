# Flyweight Pattern Implementation

## Overview

This project demonstrates the **Flyweight Pattern** implementation in C#, using a gaming scenario with different player types as a practical example. The Flyweight pattern minimizes memory usage by sharing efficiently similar objects, especially when dealing with large numbers of objects that have common characteristics.

## What is the Flyweight Pattern?

The Flyweight Pattern is a structural design pattern that reduces memory consumption by sharing common parts of state between multiple objects, instead of storing all data in each object. It separates intrinsic state (shared) from extrinsic state (unique per object instance).

### Key Characteristics

- **Memory Optimization**: Shares common data among multiple objects to reduce memory footprint
- **Intrinsic vs Extrinsic State**: Separates shared properties from instance-specific properties
- **Factory Management**: Uses a factory to manage and reuse flyweight instances
- **Immutable Flyweights**: Shared objects should be immutable to prevent interference
- **Large Object Sets**: Most effective when dealing with large numbers of similar objects

## Real-World Example: Gaming Player System

This implementation demonstrates how different player types in a game can share common characteristics while maintaining individual state.

## Architecture

```
                    ┌─────────────────────────────────────────────────────────┐
                    │                 Flyweight                               │
                    │                  Player                                 │
                    │                                                         │
                    │  + Role: string (intrinsic)                             │
                    │  + Weapon: string (intrinsic)                           │
                    │  + AssignMission(mission): abstract void                │
                    │  + Display(x, y): abstract void (uses extrinsic state)  │
                    └─────────────────────┬───────────────────────────────────┘
                                          │ implemented by
                              ┌───────────┴───────────┐
                              ▼                       ▼
                ┌─────────────────────────┐ ┌─────────────────────────┐
                │    Terrorist            │ │   CounterTerrorist      │
                │  (Concrete Flyweight)   │ │  (Concrete Flyweight)   │
                │                         │ │                         │
                │  Role: "Terrorist"      │ │  Role: "Counter-T..."   │
                │  Weapon: "AK-47"        │ │  Weapon: "M4A1-S"       │
                │  + AssignMission()      │ │  + AssignMission()      │
                │  + Display()            │ │  + Display()            │
                └─────────────────────────┘ └─────────────────────────┘
                              ▲                       ▲
                              │ managed by            │
                              └───────────┬───────────┘
                                          ▼
                    ┌─────────────────────────────────────────────────────────┐
                    │              Flyweight Factory                          │
                    │               PlayerFactory                             │
                    │                                                         │
                    │  - players: Dictionary<string, Player>                  │
                    │  + GetPlayer(role): Player                              │
                    │    • Creates new flyweight if not exists                │
                    │    • Returns existing flyweight if already created      │
                    └─────────────────────────────────────────────────────────┘
```

## Implementation Details

### 1. Flyweight Abstract Class: Player

```csharp
internal abstract class Player
{
    public string Role { get; protected set; }      // Intrinsic state
    public string Weapon { get; protected set; }    // Intrinsic state

    public abstract void AssignMission(string mission);    // Uses extrinsic state
    public abstract void Display(int x, int y);            // Uses extrinsic state
}
```

**Purpose:** Defines the flyweight interface and stores intrinsic state (shared among instances).

### 2. Concrete Flyweight: Terrorist

```csharp
internal class Terrorist : Player
{
    public Terrorist()
    {
        Role = "Terrorist";         // Intrinsic state - shared
        Weapon = "AK-47";          // Intrinsic state - shared
    }

    public override void AssignMission(string mission)
    {
        Console.WriteLine($"{Role} assigned misson {mission}");
    }

    public override void Display(int x, int y)    // x, y are extrinsic state
    {
        Console.WriteLine($"{Role} position ({x},{y}) with {Weapon}");
    }
}
```

**Purpose:** Concrete flyweight that implements specific behavior for terrorist players.

### 3. Concrete Flyweight: CounterTerrorist

```csharp
internal class CounterTerrorist : Player
{
    public CounterTerrorist()
    {
        Role = "Counter-Terrorist";    // Intrinsic state - shared
        Weapon = "M4A1-S";            // Intrinsic state - shared
    }

    public override void AssignMission(string mission)
    {
        Console.WriteLine($"{Role} assigned mission: {mission}");
    }

    public override void Display(int x, int y)    // x, y are extrinsic state
    {
        Console.WriteLine($"{Role} at position ({x},{y}) with {Weapon}");
    }
}
```

**Purpose:** Another concrete flyweight for counter-terrorist players.

### 4. Flyweight Factory: PlayerFactory

```csharp
internal class PlayerFactory
{
    private Dictionary<string, Player> players = new();

    public Player GetPlayer(string role)
    {
        if (!players.ContainsKey(role))
        {
            Player player;
            if (role == "Terrorist")
                player = new Terrorist();
            else
                player = new CounterTerrorist();

            players[role] = player;
            Console.WriteLine($"Creating new {role} player.");
        }

        return players[role];
    }
}
```
**Purpose:** Manages flyweight instances and ensures sharing. Creates new instances only when needed.


### Example Usage

```csharp
PlayerFactory factory = new();

// First request - creates new Terrorist instance
var terrorist1 = factory.GetPlayer("Terrorist");
terrorist1.AssignMission("Plant the bomb");
terrorist1.Display(10, 20);

// Second request - creates new CounterTerrorist instance  
var counter1 = factory.GetPlayer("CounterTerrorist");
counter1.AssignMission("Defuse the bomb");
counter1.Display(5, 15);

// Third request - reuses existing Terrorist instance
var terrorist2 = factory.GetPlayer("Terrorist");
terrorist2.Display(30, 40);  // Same object, different position

// Verify it's the same instance
Console.WriteLine($"Same instance: {ReferenceEquals(terrorist1, terrorist2)}"); // True
```

## Key Concepts Demonstrated

### Intrinsic vs Extrinsic State

| State Type | Definition | Examples in Project | Storage Location |
|------------|------------|-------------------|------------------|
| **Intrinsic** | Shared among multiple objects | Role, Weapon | Inside flyweight object |
| **Extrinsic** | Unique to each context | Position (x, y), Mission | Passed as method parameters |

### 1. **Intrinsic State (Shared)**
- **Role**: "Terrorist" or "Counter-Terrorist"
- **Weapon**: "AK-47" or "M4A1-S"
- **Characteristics**: Immutable, context-independent, stored inside flyweight

### 2. **Extrinsic State (Context-specific)**
- **Position**: X and Y coordinates for each player instance
- **Mission**: Different missions for different contexts

### Memory Optimization Example

**Without Flyweight Pattern:**
```
1000 Terrorists = 1000 separate objects
- Each stores: Role, Weapon, Position, Mission
- Memory: 1000 × (full object size)
```

**With Flyweight Pattern:**
```
1000 Terrorists = 1 shared flyweight + 1000 extrinsic contexts
- Flyweight stores: Role, Weapon (shared)
- Context stores: Position, Mission (per instance)
- Memory: 1 × (flyweight) + 1000 × (context size)
```
*This implementation demonstrates how the Flyweight pattern provides an efficient way to manage large numbers of similar objects by sharing common state, dramatically reducing memory usage while maintaining object functionality.*