# Prototype Pattern Implementation

## Overview

This project demonstrates the **Prototype Pattern** implementation in C#, using a Document cloning system as a practical example. The Prototype pattern provides a way to create new objects by copying existing instances, avoiding the overhead of complex object creation and initialization.

## What is the Prototype Pattern?

The Prototype Pattern is a creational design pattern that allows you to create new objects by cloning existing instances (prototypes) rather than creating them from scratch. This pattern is particularly useful when object creation is expensive or complex.

### Key Characteristics

- **Object Cloning**: Create new objects by copying existing ones
- **Performance**: Avoid expensive initialization and setup
- **Flexibility**: Create variations of complex objects easily
- **Two Types**: Supports both shallow and deep copying
- **Interface-based**: Uses a common interface for cloning operations

### When to Use Prototype Pattern

✅ **Good for:**
- Creating objects that are expensive to initialize
- Objects with complex setup or configuration
- When you need multiple similar objects with slight variations
- Avoiding subclasses of object creators
- Runtime object creation based on existing templates

## Real-World Example: Document Cloning System

This implementation demonstrates cloning Document objects that contain text content and image references.

## Architecture

```
                    ┌─────────────────────────────────────────────────────────┐
                    │                 IPrototype<T>                           │
                    │            (Generic Interface)                          │
                    │                                                         │
                    │  + DeepClone(): T                                       │
                    │  + ShallowClone(): T                                    │
                    └─────────────────────┬───────────────────────────────────┘
                                          │ implemented by
                                          ▼
                    ┌─────────────────────────────────────────────────────────┐
                    │                   Document                              │
                    │              (Concrete Prototype)                       │
                    │                                                         │
                    │  + Title: string                                        │
                    │  + Content: string                                      │
                    │  + Images: List<string>                                 │
                    │                                                         │
                    │  + DeepClone(): Document                                │
                    │  + ShallowClone(): Document                             │
                    │  + ToString(): string                                   │
                    └─────────────────────┬───────────────────────────────────┘
                                          │ creates copies
                                          ▼
                    ┌─────────────────────────────────────────────────────────┐
                    │                 Document Copies                         │
                    │                                                         │
                    │  Deep Clone:    Independent copy with new List          │
                    │  Shallow Clone: Copy with shared List reference         │
                    └─────────────────────────────────────────────────────────┘
```

## Implementation Details

### 1. Prototype Interface

```csharp
internal interface IPrototype<T>
{
    public T DeepClone();
    public T ShallowClone();
}
```

Defines the contract for objects that can be cloned, supporting both deep and shallow copying.

### 2. Concrete Prototype: Document

```csharp
internal class Document : IPrototype<Document>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<string> Images { get; set; }
    
    public Document DeepClone()
    {
        return new Document
        {
            Title = this.Title,
            Content = this.Content,
            Images = new List<string>(this.Images)  // New list with copied elements
        };
    }
    
    public Document ShallowClone()
    {
        return (Document)this.MemberwiseClone();    // Shares the same list reference
    }
}
```

Implements both cloning strategies with different behavior for complex objects.

### 3. Demo Application

```csharp
static void Main(string[] args)
{
    // Create original document
    Document d1 = new Document
    {
        Title = "Sample Document",
        Content = "This is a sample document content.",
        Images = new List<string> { "image1.png", "image2.png" }
    };

    // Create clones
    Document d2 = d1.DeepClone();    // Independent copy
    Document d3 = d1.ShallowClone(); // Shared references

    // Modify original to demonstrate difference
    d1.Images.Add("image3.png");

    // See the results
    Console.WriteLine($"Deep clone document:\n{d2}");      // Original 2 images
    Console.WriteLine($"Shallow clone document:\n{d3}");   // All 3 images
    Console.WriteLine($"Original document:\n{d1}");        // All 3 images
}
```
## Key Concepts Demonstrated

### Deep Cloning vs Shallow Cloning

| Aspect | Deep Clone | Shallow Clone |
|--------|------------|---------------|
| **Simple Properties** | Copied independently | Copied independently |
| **Complex Objects** | New instances created | References shared |
| **Independence** | Completely independent | Partially dependent |
| **Performance** | Slower (more allocation) | Faster (reference copy) |
| **Memory Usage** | Higher (duplicate objects) | Lower (shared objects) |

### 1. **Deep Cloning**
- **Creates**: Completely independent copy
- **Complex Objects**: Creates new instances (new List)
- **Behavior**: Changes to original don't affect the clone
- **Use Case**: When you need truly independent copies

### 2. **Shallow Cloning**
- **Creates**: Copy with shared references
- **Complex Objects**: Shares the same instances (same List reference)
- **Behavior**: Changes to shared objects affect both original and clone
- **Use Case**: When sharing some data is acceptable and performance matters
---

*This implementation demonstrates how the Prototype pattern provides an efficient and flexible way to create object copies while clearly showing the important distinction between deep and shallow cloning strategies.*