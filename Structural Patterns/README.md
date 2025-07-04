# 🏗️ Structural Design Patterns

Structural design patterns focus on **object composition and relationships** between entities.  
They help organize and compose objects to form larger structures while keeping them flexible and efficient.

> ✅ These patterns help in situations where you need to create relationships between objects, adapt incompatible interfaces, or add new functionality to existing classes without changing their structure.

---

## 📦 Patterns Covered

| Pattern            | Description |
|--------------------|-------------|
| [Adapter](./AdapterPattern)          | Allows incompatible interfaces to work together by wrapping an existing class with a new interface. |
| [Bridge](./BridgePattern)     | Separates an abstraction from its implementation, allowing both to vary independently. |
| [Composite](./CompositePattern) | Composes objects into tree structures to represent part-whole hierarchies. |
| [Decorator](./DecoratorPattern)              | Attaches additional responsibilities to an object dynamically. |
| [Facade](./FacadePattern)          | Provides a simplified interface to a complex subsystem. |
| [Flyweight](./FlyweightPattern)          | Reduces the cost of creating and manipulating a large number of similar objects. |
| [Proxy](./ProxyPattern)          | Provides a surrogate or placeholder for another object to control access to it. |

---

## 🧠 When to Use Structural Patterns

- You need to create relationships between objects that are independent of their implementation.
- You want to add new functionality to existing classes without changing their structure.
- You need to adapt incompatible interfaces to work together.
- You want to control access to objects or add a layer of indirection.
- You need to compose objects into larger structures while keeping them flexible.

---

## 🔧 Key Benefits

- **Flexibility**: Easy to modify object relationships and structures
- **Reusability**: Components can be reused in different contexts
- **Maintainability**: Changes to structure don't affect existing code
- **Scalability**: Easy to extend and add new functionality
- **Decoupling**: Reduces dependencies between components 