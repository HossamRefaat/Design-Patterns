# Builder Pattern Implementation

## Overview

This project demonstrates the **Builder Pattern** implementation in C#, using a salary calculation system as a practical example. The Builder pattern allows for the step-by-step construction of complex objects, providing flexibility and control over the creation process.

## What is the Builder Pattern?

The Builder Pattern is a creational design pattern that separates the construction of a complex object from its representation. It allows you to create different types and representations of an object using the same construction code.

### Key Characteristics

- **Step-by-step construction**: Build objects incrementally
- **Fluent interface**: Method chaining for readable code
- **Flexibility**: Create different configurations of the same object type
- **Immutability**: The final product is typically immutable
- **Optional parameters**: Handle complex parameter combinations elegantly

### When to Use Builder Pattern

✅ **Good for:**
- Objects with many optional parameters
- Complex initialization logic
- Objects that require multiple steps to create
- When you want to create different representations of the same object
- Avoiding telescoping constructor anti-pattern

## Architecture

```
┌─────────────────────┐    builds    ┌──────────────────┐
│ SalaryCalculator    │◄─────────────│ SalaryCalculator │
│     Builder         │              │                  │
│                     │              │ (Complex Object) │
└─────────────────────┘              └──────────────────┘
         │                                     │
         │ creates                             │ operates on
         ▼                                     ▼
┌─────────────────────┐              ┌──────────────────┐
│   Builder Steps     │              │    Employee      │
│ - SetTaxPercentage  │              │ (Domain Object)  │
│ - SetBonusPercentage│              │                  │
│ - SetEducationPkg   │              │                  │
│ - SetTransportation │              │                  │
│ - SetNotifications  │              │                  │
└─────────────────────┘              └──────────────────┘
```
## Implementation Details
S
### 1. Product: SalaryCalculator

The complex object being constructed with multiple configuration options:

```csharp
internal class SalaryCalculator
{
    public SalaryCalculator(int taxPercentage = 0, decimal bonusPercentage = 0, 
                          decimal educationPackage = 0, decimal transportation = 0, 
                          bool sendPayslipToEmployee = true, bool postResultsToGl = true)
    {
        // Initialization with default values
    }
    
    public decimal CalculateSalary(Employee employee)
    {
        // Complex calculation logic
        decimal taxAmount = employee.BasicSalary * TaxPercentage / 100;
        decimal bonusAmount = employee.BasicSalary * BonusPercentage / 100;
        decimal totalSalary = employee.BasicSalary - taxAmount + bonusAmount + 
                             EducationPackage + Transportation;
        return totalSalary;
    }
}
```

### 2. Builder: SalaryCalculatorBuilder

Provides fluent interface for step-by-step construction:

```csharp
internal class SalaryCalculatorBuilder
{
    private int taxPercentage = 0;
    private decimal bonusPercentage = 0;
    private decimal educationPackage = 0;
    private decimal transportation = 0;
    private bool sendPayslipToEmployee = true;
    private bool postResultsToGl = true;

    public SalaryCalculatorBuilder SetTaxPercentage(int taxPercentage)
    {
        this.taxPercentage = taxPercentage;
        return this; // Fluent interface
    }
    
    // Additional builder methods...
    
    public SalaryCalculator Build()
    {
        return new SalaryCalculator(taxPercentage, bonusPercentage, 
                                   educationPackage, transportation, 
                                   sendPayslipToEmployee, postResultsToGl);
    }
}
```

### 3. Domain Model: Employee

Simple data object representing an employee:

```csharp
internal class Employee
{
    public Employee(string name, string email, decimal basicSalary)
    {
        Name = name;
        Email = email;
        BasicSalary = basicSalary;
    }
    
    public decimal BasicSalary { get; }
    public string Name { get; }
    public string Email { get; }
}
```
### Example Usage

```csharp
// Using the Builder Pattern
var builder = new SalaryCalculatorBuilder();

var calculator = builder
    .SetTaxPercentage(20)           // Fluent interface
    .SetBonusPercentage(15)         // Method chaining
    .SetEducationPackage(2000)      // Step-by-step construction
    .SetTransportation(1000)
    .SetSendPayslipToEmployee(true)
    .Build();                       // Final product creation

var employee = new Employee("Hossam Mazmaz", "hossam@example.com", 20000);
var salary = calculator.CalculateSalary(employee);
```

## Key Benefits Demonstrated

### 1. **Fluent Interface**
- Method chaining provides readable and intuitive API
- Each method returns the builder instance for chaining

### 2. **Flexible Construction**
- Optional parameters can be set in any order
- Default values ensure sensible behavior when options are omitted

### 3. **Separation of Concerns**
- Builder handles construction logic
- Product handles business logic
- Clear separation of responsibilities

### 4. **Immutability**
- Once built, the SalaryCalculator is immutable
- Thread-safe and predictable behavior

### 5. **Complex Parameter Handling**
- Eliminates telescoping constructor problem
- Clear, self-documenting parameter setting

## Real-World Applications

The Builder pattern is commonly used in:

- **Configuration objects**: Application settings, connection strings
- **SQL query builders**: Constructing complex database queries
- **HTTP request builders**: Building REST API requests
- **Test data builders**: Creating test objects with specific configurations
- **Document builders**: PDF, HTML, or XML document construction

---

*This implementation demonstrates how the Builder pattern can make complex object construction more manageable and maintainable in real-world applications.*