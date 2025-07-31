# Proxy Design Pattern

## What is the Proxy Design Pattern?

The **Proxy Design Pattern** is a structural design pattern that provides a placeholder or surrogate for another object to control access to it. Think of it like a security guard at a building entrance - the guard controls who can enter the building before allowing them to access the real services inside.

## Key Components

1. **Subject Interface** (`Internet`) - Defines the common interface for both the real object and proxy
2. **Real Subject** (`RealInternetConnection`) - The actual object that does the real work
3. **Proxy** (`ProxyInternet`) - Controls access to the real subject and can add additional functionality
4. **Client** (`Program`) - Uses the proxy to access the real subject

## When to Use the Proxy Pattern?

- **Access Control**: Control who can access a resource (like our internet access example)
- **Lazy Loading**: Create expensive objects only when needed
- **Caching**: Store results to avoid repeated expensive operations
- **Logging**: Track when and how objects are accessed
- **Remote Objects**: Represent objects that exist in different locations

## How Our Implementation Works

### 1. The Interface (`Internet.cs`)
```csharp
public interface Internet
{
    void GetInternetAccess(Employee employee);   
}
```
This defines what both the real internet connection and proxy must implement.

### 2. The Real Subject (`RealInternetConnection.cs`)
```csharp
public class RealInternetConnection : Internet
{
    public void GetInternetAccess(Employee employee)
    {
        Console.WriteLine($"Granted Internet Permission for {employee.Name}");
    }
}
```
This is the actual service that provides internet access.

### 3. The Proxy (`ProxyInternet.cs`)
```csharp
internal class ProxyInternet : Internet
{
    public void GetInternetAccess(Employee employee)
    {
        if(employee.SecurityLevel > 5)
        {
           new RealInternetConnection().GetInternetAccess(employee);
        }
        else
        {
            Console.WriteLine("Access Denied: Insufficient Security Level");
        }
    }
}
```
The proxy checks the employee's security level before allowing access to the real internet connection.

### 4. Usage Example (`Program.cs`)
```csharp
Employee employee1 = new Employee { Name = "Alice", SecurityLevel = 4 };
Internet internet = new ProxyInternet();

internet.GetInternetAccess(employee1); // Output: "Access Denied: Insufficient Security Level"

employee1.SecurityLevel = 6;
internet.GetInternetAccess(employee1); // Output: "Granted Internet Permission for Alice"
```

## Benefits of Using Proxy Pattern

1. **Security**: Controls access to sensitive resources
2. **Performance**: Can implement caching or lazy loading
3. **Transparency**: Client doesn't know if they're using a proxy or real object
4. **Flexibility**: Can add features without changing the real object
5. **Separation of Concerns**: Security logic is separate from business logic

## Real-World Examples

- **Web Proxies**: Filter and cache web requests
- **ATM Machines**: Act as proxies to your bank account
- **Smart Pointers**: Control access to memory in programming
- **Virtual Images**: Load large images only when needed
- **Firewall**: Controls network access to servers

## Summary

The Proxy Pattern is like having a helpful assistant that:
- Checks if you're allowed to access something
- Handles the request for you if you're authorized
- Can add extra features like logging or caching
- Protects the real object from direct access

In our example, the `ProxyInternet` acts as a security checkpoint that only allows employees with high enough security levels to access the internet, while completely hiding the complexity of the real internet connection from the client code. 