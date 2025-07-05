# 🧠 Behavioral Design Patterns

Behavioral design patterns focus on **communication between objects** and **responsibility assignment**.  
They help define how objects interact and communicate with each other, making the system more flexible and maintainable.

> ✅ These patterns help in situations where you need to manage complex communication between objects, assign responsibilities dynamically, or create flexible algorithms that can be easily modified.

---

## 📦 Patterns Covered

| Pattern            | Description |
|--------------------|-------------|
| [Chain of Responsibility](./ChainOfResponsibilityPattern) | Passes requests along a chain of handlers until one handles it. |
| [Command](./CommandPattern)     | Encapsulates a request as an object, allowing parameterization of clients. |
| [Iterator](./IteratorPattern) | Provides a way to access elements of a collection without exposing its underlying representation. |
| [Mediator](./MediatorPattern) | Reduces coupling between components by making them communicate indirectly through a mediator. |
| [Memento](./MementoPattern)              | Captures and externalizes an object's internal state for later restoration. |
| [Observer](./ObserverPattern)          | Defines a one-to-many dependency between objects so that when one object changes state, all dependents are notified. |
| [State](./StatePattern)          | Allows an object to alter its behavior when its internal state changes. |
| [Strategy](./StrategyPattern)          | Defines a family of algorithms, encapsulates each one, and makes them interchangeable. |
| [Template Method](./TemplateMethodPattern)          | Defines the skeleton of an algorithm in a method, deferring some steps to subclasses. |
| [Visitor](./VisitorPattern)          | Separates an algorithm from the object structure on which it operates. |

---

## 🧠 When to Use Behavioral Patterns

- You need to manage complex communication between multiple objects.
- You want to assign responsibilities to objects dynamically at runtime.
- You need to create flexible algorithms that can be easily modified or extended.
- You want to reduce coupling between objects while maintaining their communication.
- You need to implement undo/redo functionality or state management.
- You want to create event-driven systems with loose coupling.
- You need to implement different algorithms that can be swapped at runtime.

---

## 🔧 Key Benefits

- **Loose Coupling**: Objects communicate without knowing each other's concrete classes
- **Flexibility**: Easy to modify communication patterns and algorithms
- **Extensibility**: New behaviors can be added without changing existing code
- **Maintainability**: Clear separation of responsibilities makes code easier to understand
- **Reusability**: Behavioral components can be reused across different contexts
- **Testability**: Individual behaviors can be tested in isolation

---

## 🎯 Common Use Cases

- **Event Handling**: Observer pattern for implementing event-driven architectures
- **Undo/Redo Systems**: Command pattern with Memento for state restoration
- **Algorithm Selection**: Strategy pattern for runtime algorithm selection
- **Request Processing**: Chain of Responsibility for handling requests in sequence
- **State Management**: State pattern for managing object state transitions
- **Collection Iteration**: Iterator pattern for traversing collections
- **Cross-Object Communication**: Mediator pattern for reducing object dependencies 