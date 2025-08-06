# Abstract Factory Pattern Implementation

## Overview

This project demonstrates the **Abstract Factory Pattern** implementation in C#, using a **Cross-Platform GUI System** as a practical example. The Abstract Factory pattern provides an interface for creating families of related or dependent objects without specifying their concrete classes.

## What is the Abstract Factory Pattern?

The Abstract Factory Pattern is a creational design pattern that solves the problem of creating entire product families without coupling the code to concrete classes of those products. It's like having different factories, each specialized in creating a complete set of related products that work together.

### Key Characteristics

- **Families of Related Objects**: Creates multiple related products that are designed to work together
- **Factory Interface**: Provides a common interface for creating different families
- **Product Families**: Ensures all products from one factory are compatible
- **Platform Independence**: Client code doesn't depend on specific platform implementations
- **Consistency**: Guarantees that products from the same family work together

### When to Use Abstract Factory Pattern

✅ **Good for:**
- Creating families of related products that must work together
- Supporting multiple platforms or operating systems
- When you need to enforce consistency across product families
- Systems that should be independent of how products are created
- Cross-platform applications with platform-specific components

## Real-World Example: Cross-Platform GUI System

This implementation demonstrates creating platform-specific GUI components (Button, Checkbox) for different operating systems (Windows, Mac). This example is perfect because:

## Architecture

```
                    ┌─────────────────────────────────────────────────────────┐
                    │                    Client                               │
                    │                (Application)                            │
                    └─────────────────────┬───────────────────────────────────┘
                                          │ uses
                                          ▼
                    ┌─────────────────────────────────────────────────────────┐
                    │              Abstract Factory                           │
                    │                IGUIFactory                              │
                    │  + CreateButton(): IButton                              │
                    │  + CreateCheckbox(): ICheckbox                          │
                    └─────────────────────┬───────────────────────────────────┘
                                          │ implemented by
                            ┌─────────────┴─────────────┐
                            ▼                           ▼
              ┌─────────────────────────┐  ┌─────────────────────────┐
              │   WindowsFactory        │  │     MacFactory          │
              │                         │  │                         │
              │ + CreateButton()        │  │ + CreateButton()        │
              │ + CreateCheckbox()      │  │ + CreateCheckbox()      │
              └─────────────────────────┘  └─────────────────────────┘
                          │                              │
                   creates│                              │creates
                          ▼                              ▼
              ┌─────────────────────────┐  ┌─────────────────────────┐
              │   Windows Products      │  │     Mac Products        │
              │                         │  │                         │
              │  • WindowsButton        │  │  • MacButton            │
              │  • WindowsCheckbox      │  │  • MacCheckbox          │
              └─────────────────────────┘  └─────────────────────────┘
                          │                              │
                implements│                              │implements
                          ▼                              ▼
              ┌─────────────────────────────────────────────────────────┐
              │              Abstract Products                          │
              │                                                         │
              │  IButton                    ICheckbox                   │
              │  + Render()                 + Render()                  │
              └─────────────────────────────────────────────────────────┘
```

## Implementation Details

### 1. Abstract Product Interfaces

```csharp
internal interface IButton
{
    void Render();
}

internal interface ICheckbox
{
    void Render();
}
```
Define the common interface for all platform-specific product variants.

### 2. Concrete Products

**Windows Products:**
```csharp
internal class WindowsButton : IButton
{
    public void Render() => Console.WriteLine("Rendering Windows Button.");
}

internal class WindowsCheckbox : ICheckbox
{
    public void Render() => Console.WriteLine("Rendering Windows Checkbox.");
}
```

**Mac Products:**
```csharp
internal class MacButton : IButton
{
    public void Render() => Console.WriteLine("Rendering Mac Button.");
}

internal class MacCheckbox : ICheckbox
{
    public void Render() => Console.WriteLine("Rendering Mac Checkbox.");
}
```

### 3. Abstract Factory Interface

```csharp
internal interface IGUIFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}
```

Defines the contract for creating families of GUI components.

### 4. Concrete Factories

**Windows Factory:**
```csharp
internal class WindowsFactory : IGUIFactory
{
    public IButton CreateButton() => new WindowsButton();
    public ICheckbox CreateCheckbox() => new WindowsCheckbox();
}
```

**Mac Factory:**
```csharp
internal class MacFactory : IGUIFactory
{
    public IButton CreateButton() => new MacButton();
    public ICheckbox CreateCheckbox() => new MacCheckbox();
}
```

Each factory creates a complete family of platform-specific products.



### 5. Client Application

```csharp
internal class Application
{
    private IButton button;
    private ICheckbox checkbox;

    public Application(IGUIFactory factory)
    {
        button = factory.CreateButton();    // Platform-specific button
        checkbox = factory.CreateCheckbox(); // Platform-specific checkbox
    }

    public void Run()
    {
        button.Render();    // Renders platform-appropriate button
        checkbox.Render();  // Renders platform-appropriate checkbox
    }
}
```

### Example Usage

```csharp
// Create Windows-based application
IGUIFactory windowsFactory = new WindowsFactory();
var windowsApp = new Application(windowsFactory);
windowsApp.Run();

// Switch to Mac platform - same client code!
IGUIFactory macFactory = new MacFactory();
var macApp = new Application(macFactory);
macApp.Run();
```
## Extending the Implementation

Consider these enhancements:

- **New Platforms**: Add LinuxFactory, AndroidFactory, iOSFactory
- **New Components**: Add ITextBox, IMenu, IDialog interfaces
- **Platform Detection**: Auto-detect platform and choose factory
- **Configuration**: Load factory choice from config files
- **Styling**: Add platform-specific styling and themes

---

*This implementation demonstrates how the Abstract Factory pattern enables clean, platform-independent code while ensuring that related components work together seamlessly.*