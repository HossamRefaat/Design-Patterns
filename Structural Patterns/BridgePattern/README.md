# Bridge Pattern Implementation

## Overview

This project demonstrates the **Bridge Pattern** implementation in C#, using a cross-platform UI system as a practical example. The Bridge pattern decouples an abstraction from its implementation, allowing both to vary independently without affecting each other.

## What is the Bridge Pattern?

The Bridge Pattern is a structural design pattern that separates the abstraction (what you want to do) from the implementation (how you do it). This separation allows both the abstraction and implementation to evolve independently, promoting flexibility and maintainability.

### Key Characteristics

- **Separation of Concerns**: Abstraction and implementation are separate hierarchies
- **Runtime Binding**: Implementation can be chosen and changed at runtime
- **Independent Evolution**: Both abstractions and implementations can vary independently
- **Platform Independence**: Perfect for cross-platform development
- **Composition over Inheritance**: Uses composition to connect abstractions with implementations


## Real-World Example: Cross-Platform UI System

This implementation demonstrates how UI components (abstraction) can work with different operating systems (implementations) independently.


## Architecture

```
                    ┌─────────────────────────────────────────────────────────┐
                    │                 Abstraction                             │
                    │                 CommonUI                                │
                    │                                                         │
                    │  # operatingSystem: IOperatingSystem                    │
                    │  + CommonUI(IOperatingSystem)                           │
                    │  + Click(): abstract void                               │
                    └─────────────────────┬───────────────────────────────────┘
                                          │ extended by
                                          ▼
                    ┌─────────────────────────────────────────────────────────┐
                    │             Refined Abstraction                         │
                    │                   Button                                │
                    │                                                         │
                    │  + Button(IOperatingSystem)                             │
                    │  + Click(): void                                        │
                    └─────────────────────┬───────────────────────────────────┘
                                          │ uses
                                          ▼
                    ┌─────────────────────────────────────────────────────────┐
                    │                Implementor                              │
                    │             IOperatingSystem                            │
                    │                                                         │
                    │  + DoOperation(): void                                  │
                    └─────────────────────┬───────────────────────────────────┘
                                          │ implemented by
                            ┌─────────────┼─────────────┐
                            ▼             ▼             ▼
              ┌─────────────────┐ ┌─────────────────┐ ┌─────────────────┐
              │    Windows      │ │      Mac        │ │     Linux       │
              │                 │ │                 │ │                 │
              │ + DoOperation() │ │ + DoOperation() │ │ + DoOperation() │
              └─────────────────┘ └─────────────────┘ └─────────────────┘
```

## Implementation Details

### 1. Abstraction: CommonUI

```csharp
abstract class CommonUI
{
    protected IOperatingSystem operatingSystem;

    protected CommonUI(IOperatingSystem operatingSystem)
    {
        this.operatingSystem = operatingSystem;
    }

    public abstract void Click();
}
```

**Purpose:** Defines the high-level interface and maintains a reference to the implementor.

### 2. Refined Abstraction: Button

```csharp
internal class Button : CommonUI
{
    public Button(IOperatingSystem operatingSystem) : base(operatingSystem)
    {
    }
   
    public override void Click()
    {
        operatingSystem.DoOperation();  // Delegates to implementor
    }
}
```

**Purpose:** Extends the abstraction with specific UI component logic.

### 3. Implementor Interface: IOperatingSystem

```csharp
internal interface IOperatingSystem
{
    public void DoOperation();
}
```

**Purpose:** Defines the interface for concrete implementations (operating systems).

### 4. Concrete Implementors

**Windows Implementation:**
```csharp
internal class Windows : IOperatingSystem
{
    public void DoOperation()
    {
        Console.WriteLine("Doing operations on windows platform");
    }
}
```

**Mac Implementation:**
```csharp
internal class Mac : IOperatingSystem
{
    public void DoOperation()
    {
        Console.WriteLine("Doing operations on mac platform");
    }
}
```

**Linux Implementation:**
```csharp
internal class Linux : IOperatingSystem
{
    public void DoOperation()
    {
        Console.WriteLine("Doing operations on linux platform");
    }
}
```
### Example Usage

```csharp
// Create UI component with different operating systems
var windowsButton = new Button(new Windows());
var macButton = new Button(new Mac());
var linuxButton = new Button(new Linux());

// Same interface, different behaviors
windowsButton.Click();  // "Doing operations on windows platform"
macButton.Click();      // "Doing operations on mac platform"
linuxButton.Click();    // "Doing operations on linux platform"

// Runtime platform switching
Button dynamicButton = new Button(new Windows());
dynamicButton.Click();  // Windows behavior

// Change implementation at runtime
dynamicButton = new Button(new Mac());
dynamicButton.Click();  // Mac behavior
```

## Key Benefits Demonstrated

### 1. **Decoupling**
- UI components are independent of operating system implementations
- Changes to OS implementations don't affect UI code
- Changes to UI abstractions don't affect OS code

### 2. **Runtime Flexibility**
- Can switch operating system implementations at runtime
- Dynamic binding between abstraction and implementation
- Flexible configuration based on environment

### 3. **Independent Extension**
- Add new UI components without changing OS implementations
- Add new operating systems without changing UI components
- Both hierarchies can evolve separately

### 4. **Code Reusability**
- Same UI components work across all platforms
- Platform implementations can be shared across UI components
- Reduced code duplication

### 5. **Platform Abstraction**
- Client code doesn't need to know about specific platforms
- Clean separation between what and how
- Easy testing with mock implementations

## Extending the Implementation

Consider these enhancements:

### Add More UI Components

```csharp
// Add TextBox, CheckBox, etc.
internal class TextBox : CommonUI
{
    public TextBox(IOperatingSystem operatingSystem) : base(operatingSystem) { }
    
    public override void Click()
    {
        Console.WriteLine("TextBox focus:");
        operatingSystem.DoOperation();
    }
}
```

### Add More Operating Systems

```csharp
// Add Android, iOS, etc.
internal class Android : IOperatingSystem
{
    public void DoOperation()
    {
        Console.WriteLine("Doing operations on android platform");
    }
}
```

### Add More Sophisticated Operations

```csharp
interface IOperatingSystem
{
    void DoOperation();
    void RenderUI();
    void HandleInput();
    void PlaySound();
}
```

*This implementation demonstrates how the Bridge pattern provides a clean way to separate high-level abstractions from low-level implementations, enabling both to evolve independently while maintaining flexibility and extensibility.*