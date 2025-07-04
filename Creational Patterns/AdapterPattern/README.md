# Adapter Pattern Example: Payroll System Integration

## What is the Adapter Design Pattern?

The Adapter pattern is a structural design pattern that allows incompatible interfaces to work together. It acts as a bridge between two incompatible interfaces by wrapping an existing class with a new interface.

**Key characteristics:**
- Converts the interface of a class into another interface that clients expect
- Allows classes to work together that couldn't otherwise because of incompatible interfaces
- Promotes loose coupling between the client and the adapted class

## Problem Statement

### The Challenge
We have two different systems with incompatible data structures:

1. **Client System (AdapterPattern Project):**
   - `Employee` class with `FirstName`, `SecondName`, `LastName` properties
   - `PayItem` class with `Name`, `Value`, and `IsDeduction` properties

2. **Payroll System API (PayrollSystem Project):**
   - `Employee` class with only `FullName` property
   - `PayItem` class with only `Name` and `Value` properties
   - No `IsDeduction` property concept

### The Business Requirement
The business wants to represent pay items as either deductions or additions using an `IsDeduction` property:
- When `IsDeduction = true`, the value should be negative
- When `IsDeduction = false`, the value should be positive

## How the Adapter Pattern Solves This Problem

### Solution Overview
We use adapter classes to transform the client's data structure into the format expected by the Payroll System API.

### Implementation Details

#### 1. PayrollSystemEmployeeAdapter
```csharp
class PayrollSystemEmployeeAdapter
{
    private readonly Employee employee;
    private readonly IEnumerable<PayrollSystemPayItemAdapter> payItems;

    public PayrollSystemEmployeeAdapter(Employee employee)
    {
        this.employee = employee;
        this.payItems = employee.PayItems.Select(pi => new PayrollSystemPayItemAdapter(pi)).ToList();
    }

    public string FullName => $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
    public IEnumerable<PayrollSystemPayItemAdapter> PayItems => payItems;
}
```

**Purpose:** Adapts the client's `Employee` object by:
- Converting separate name fields into a single `FullName` property
- Wrapping each `PayItem` with a `PayrollSystemPayItemAdapter`

#### 2. PayrollSystemPayItemAdapter
```csharp
public class PayrollSystemPayItemAdapter
{
    private readonly PayItem payItem;

    public PayrollSystemPayItemAdapter(PayItem payItem)
    {
        this.payItem = payItem;
    }

    public string Name => payItem.Name;
    public decimal Value => payItem.IsDeduction ? -1 * payItem.Value : payItem.Value;
}
```

**Purpose:** Adapts the client's `PayItem` object by:
- Preserving the `Name` property
- Converting the `Value` based on the `IsDeduction` property:
  - If `IsDeduction = true`, the value becomes negative
  - If `IsDeduction = false`, the value remains positive

### Usage in the Application

```csharp
var employeeAdapter = new PayrollSystemEmployeeAdapter(employee);
request.Content = new StringContent(
    JsonSerializer.Serialize(employeeAdapter),
    System.Text.Encoding.UTF8,
    "application/json"
);
```

## Benefits of This Solution

1. **Separation of Concerns:** The client system doesn't need to know about the Payroll System's internal structure
2. **Maintainability:** Changes to either system's interface can be handled by updating the adapter
3. **Reusability:** The adapter can be reused for other integrations
4. **Testability:** Each component can be tested independently

## Example Data Flow

**Input (Client System):**
```json
{
  "firstName": "John",
  "secondName": "A.",
  "lastName": "Doe",
  "payItems": [
    { "name": "Base Salary", "value": 3000, "isDeduction": false },
    { "name": "Medical Insurance", "value": 200, "isDeduction": true }
  ]
}
```

**Output (After Adapter):**
```json
{
  "fullName": "John A. Doe",
  "payItems": [
    { "name": "Base Salary", "value": 3000 },
    { "name": "Medical Insurance", "value": -200 }
  ]
}
```

This implementation demonstrates how the Adapter pattern can seamlessly integrate systems with different data structures while maintaining clean, maintainable code. 