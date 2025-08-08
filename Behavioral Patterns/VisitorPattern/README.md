# Visitor Pattern Implementation

## Overview

This project demonstrates the **Visitor Pattern** implementation in C#, using a geometric shapes system as a practical example. The Visitor pattern allows you to define new operations on objects without changing their classes, separating algorithms from the object structure on which they operate.

## What is the Visitor Pattern?

The Visitor Pattern is a behavioral design pattern that represents an operation to be performed on elements of an object structure. It lets you define new operations without changing the classes of the elements on which it operates, promoting extensibility and maintaining the Open/Closed Principle.

### Key Characteristics

- **Operation Separation**: Separates algorithms from the object structure
- **Double Dispatch**: Uses method overloading to determine the correct operation at runtime  
- **Extensibility**: Easy to add new operations without modifying existing element classes
- **Type Safety**: Compile-time checking ensures all element types are handled
- **Centralized Logic**: Related operations are grouped together in visitor classes

## Real-World Example: Geometric Shapes Area Calculation

This implementation demonstrates how different geometric shapes can accept visitors that perform operations (like area calculation) without modifying the shape classes themselves. 

## Architecture

```
                    ┌─────────────────────────────────────────────────────────┐
                    │                    Element                              │
                    │                   IShape                                │
                    │                                                         │
                    │  + Accept(IShapeVisitor): void                          │
                    └─────────────────────┬───────────────────────────────────┘
                                          │ implemented by
                              ┌───────────┼───────────┐
                              ▼           ▼           ▼
                ┌─────────────────┐ ┌─────────────┐ ┌─────────────────┐
                │    Circle       │ │  Rectangle  │ │    Triangle     │
                │ (Concrete Elem) │ │(Concrete El)│ │ (Concrete Elem) │
                │                 │ │             │ │                 │
                │ + Radius        │ │ + Width     │ │ + Base          │
                │ + Accept()      │ │ + Height    │ │ + Height        │
                │                 │ │ + Accept()  │ │ + Accept()      │
                └─────────────────┘ └─────────────┘ └─────────────────┘
                          │               │               │
                          │ accepts       │ accepts       │ accepts
                          ▼               ▼               ▼
                    ┌─────────────────────────────────────────────────────────┐
                    │                   Visitor                               │
                    │               IShapeVisitor                             │
                    │                                                         │
                    │  + Visit(Circle): void                                  │
                    │  + Visit(Rectangle): void                               │
                    │  + Visit(Triangle): void                                │
                    └─────────────────────┬───────────────────────────────────┘
                                          │ implemented by
                                          ▼
                    ┌─────────────────────────────────────────────────────────┐
                    │             Concrete Visitor                            │
                    │           ShapeAreaCalculator                           │
                    │                                                         │
                    │  + Visit(Circle): void     → Calculate circle area      │
                    │  + Visit(Rectangle): void  → Calculate rectangle area   │
                    │  + Visit(Triangle): void   → Calculate triangle area    │
                    └─────────────────────────────────────────────────────────┘
```
## Implementation Details

### 1. Element Interface: IShape

```csharp
internal interface IShape
{
    void Accept(IShapeVisitor visitor);
}
```
**Purpose:** Defines the element interface with the Accept method that takes a visitor.

### 2. Concrete Elements: Circle, Rectangle, Triangle

**Circle Implementation:**
```csharp
internal class Circle : IShape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public void Accept(IShapeVisitor visitor)
    {
        visitor.Visit(this);  // Double dispatch - calls visitor's Visit(Circle)
    }
}
```

**Rectangle Implementation:**
```csharp
internal class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public void Accept(IShapeVisitor visitor)
    {
        visitor.Visit(this);  // Double dispatch - calls visitor's Visit(Rectangle)
    }
}
```

**Triangle Implementation:**
```csharp
internal class Triangle : IShape
{
    public double Base { get; set; }
    public double Height { get; set; }

    public Triangle(double @base, double height)
    {
        this.Base = @base;
        this.Height = height;
    }

    public void Accept(IShapeVisitor visitor)
    {
        visitor.Visit(this);  // Double dispatch - calls visitor's Visit(Triangle)
    }
}
```
**Purpose:** Concrete elements that implement the Accept method and store shape-specific data.

### 3. Visitor Interface: IShapeVisitor

```csharp
internal interface IShapeVisitor
{
    void Visit(Circle circle);
    void Visit(Rectangle rectangle);
    void Visit(Triangle triangle);
}
```
**Purpose:** Defines the visitor interface with overloaded Visit methods for each element type.

### 4. Concrete Visitor: ShapeAreaCalculator

```csharp
internal class ShapeAreaCalculator : IShapeVisitor
{
    public void Visit(Circle circle)
    {
        double area = Math.PI * circle.Radius * circle.Radius;
        Console.WriteLine($"Circle Area: {area:F2}");
    }

    public void Visit(Triangle triangle)
    {
        double area = 0.5 * triangle.Base * triangle.Height;
        Console.WriteLine($"Triangle Area: {area:F2}");
    }

    public void Visit(Rectangle rectangle)
    {
        double area = rectangle.Width * rectangle.Height;
        Console.WriteLine($"Rectangle Area: {area:F2}");
    }
}
```

**Purpose:** Implements specific operations (area calculation) for each shape type.

### 5. Program.cs

```csharp
// Create shapes
List<IShape> shapes = new List<IShape>
{   
    new Circle(3),           // Radius = 3
    new Rectangle(4, 5),     // Width = 4, Height = 5
    new Triangle(6, 2)       // Base = 6, Height = 2
};

// Create visitor
var areaVisitor = new ShapeAreaCalculator();

// Apply visitor to all shapes
foreach (var shape in shapes)
{
    shape.Accept(areaVisitor);  // Double dispatch determines correct Visit method
}
---
```
*This implementation demonstrates how the Visitor pattern provides a clean way to separate operations from object structures, enabling extensible and maintainable code while preserving type safety through double dispatch.*
