# Singleton Pattern Example: Currency Converter

## What is the Singleton Design Pattern?
The Singleton design pattern ensures that a class has only one instance throughout the application's lifecycle and provides a global point of access to that instance. This is useful when exactly one object is needed to coordinate actions across the system, such as configuration managers, logging, or connection pools.

**Key characteristics:**
- Only one instance of the class exists.
- The instance is globally accessible.
- The class controls its own instantiation.

## About This Project
This project demonstrates the Singleton pattern using a simple currency converter application. The application allows users to convert amounts between different currencies using predefined exchange rates. The core logic is implemented in C#.

### Project Structure
- `Program.cs`: The main entry point, handles user input and output.
- `CurrancyConverter.cs`: Implements the Singleton pattern and contains the conversion logic.
- `ExchangeRate.cs`: Represents an exchange rate between two currencies.

## How is the Singleton Pattern Implemented Here?
The `CurrancyConverter` class is implemented as a Singleton:
- **Private Constructor:** Prevents external instantiation.
- **Static Instance Property:** Provides a global access point to the single instance.
- **Double-Checked Locking:** Ensures thread-safe lazy initialization of the instance.

### Key Implementation Details
```csharp
public class CurrancyConverter
{
    private static object _lock = new();
    private static CurrancyConverter _instance;
    private CurrancyConverter() { /* ... */ }

    public static CurrancyConverter Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new();
                    }
                }
            }
            return _instance;
        }
    }
    // ...
}
```
- The `Instance` property checks if an instance exists; if not, it creates one inside a lock to ensure thread safety.
- The constructor is private, so the only way to get an instance is through the `Instance` property.

### Usage in the Project
In `Program.cs`, the Singleton instance is used to perform currency conversions:
```csharp
var exchangedAmount = CurrancyConverter.Instance.Convert(baseCurrency, targetCurrency, amount);
```
This ensures that all conversions use the same instance of `CurrancyConverter`, and the exchange rates are loaded only once.

## Summary
This project is a practical demonstration of the Singleton pattern, ensuring a single, shared instance of the currency converter throughout the application's lifetime. This approach is ideal for scenarios where shared state or resources are required, such as configuration, logging, or, as shown here, currency conversion logic. 