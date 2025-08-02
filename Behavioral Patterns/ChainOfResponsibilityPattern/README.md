# Chain of Responsibility Design Pattern

## What is the Chain of Responsibility Design Pattern?

The **Chain of Responsibility Design Pattern** is a behavioral pattern that allows you to pass requests along a chain of handlers until one of them handles the request. Think of it like a corporate approval process - when you submit an expense report, it goes through different levels of management until someone with the right authority approves or rejects it.

## Key Benefits

- **Decouples sender from receiver** - The sender doesn't need to know who will handle the request
- **Dynamic chain composition** - You can add, remove, or reorder handlers at runtime
- **Single Responsibility** - Each handler focuses on one specific type of request
- **Flexible request handling** - Multiple handlers can process the same request
- **Fail-safe processing** - If no handler can process the request, it can be handled gracefully

## Key Components

1. **Handler Interface** (`IApprovalHandler`) - Defines the contract for handling requests
2. **Abstract Handler** (`ApprovalHandler`) - Provides common functionality for chaining handlers
3. **Concrete Handlers** (`TeamLeaderApprovalHandler`, `TechnicalManagerApprovalHandler`, etc.) - Implement specific handling logic
4. **Request** (`VacationRequest`) - Contains the data that needs to be processed
5. **Client** (`Program`) - Creates the chain and initiates the request

## Our Implementation: Vacation Request Approval System

This project demonstrates the pattern through a corporate vacation approval workflow where different management levels have different approval authorities.

### Business Rules
```
• Team Leader can approve Developer requests up to 3 days
• Technical Manager can approve Developer requests > 3 days and Team Leader requests  
• CTO can approve Technical Manager requests
• CEO can approve CTO requests
```

### 1. Handler Interface (`IApprovalHandler.cs`)
```csharp
internal interface IApprovalHandler
{
    void SetNextHandler(IApprovalHandler handler);
    void Process(VacationRequest request);   
}
```
This defines the contract that all handlers must implement.

### 2. Abstract Base Handler (`ApprovalHandler.cs`)
```csharp
internal abstract class ApprovalHandler : IApprovalHandler
{
    private IApprovalHandler _nextHandler;
    public abstract void Process(VacationRequest request);

    public void SetNextHandler(IApprovalHandler handler)
    {
        _nextHandler = handler;
    }

    protected void CallNext(VacationRequest request)
    {
        if (_nextHandler != null)
        {
            _nextHandler.Process(request);
        }
        else
        {
            Console.WriteLine($"No handler found for request from {request.Employee.Name} for {request.TotalDays} days.");
        }
    }
}
```
This provides common functionality for managing the chain and passing requests to the next handler.

### 3. Concrete Handlers

#### Team Leader Handler
```csharp
internal class TeamLeaderApprovalHandler : ApprovalHandler
{
    public override void Process(VacationRequest request)
    {
        // Can approve Developer requests up to 3 days
        if (request.Employee.JobTitle == JobTitle.Developer && request.TotalDays <= 3)
        {
            Console.WriteLine($"Team Leader approved vacation request for {request.Employee.Name}");
        }
        else
        {
            CallNext(request); // Pass to next handler
        }
    }
}
```

#### Technical Manager Handler
```csharp
internal class TechnicalManagerApprovalHandler : ApprovalHandler
{
    public override void Process(VacationRequest request)
    {
        // Can approve Developer requests > 3 days and Team Leader requests
        if (request.Employee.JobTitle == JobTitle.Developer && request.TotalDays > 3)
        {
            Console.WriteLine($"Technical Manager approved vacation request for {request.Employee.Name}");
        }
        else if (request.Employee.JobTitle == JobTitle.TeamLeader)
        {
            Console.WriteLine($"Technical Manager approved vacation request for Team Leader {request.Employee.Name}");
        }
        else
        {
            CallNext(request);
        }
    }
}
```

#### CTO Handler
```csharp
internal class CTOApprovalHandler : ApprovalHandler
{
    public override void Process(VacationRequest request)
    {
        // Can approve Technical Manager requests
        if(request.Employee.JobTitle == JobTitle.TechnicalManager)
        {
            Console.WriteLine($"CTO approved vacation request for {request.Employee.Name}");
        }
        else
        {
            CallNext(request);
        }
    }
}
```

#### CEO Handler
```csharp
internal class CEOApprovalHandler : ApprovalHandler
{
    public override void Process(VacationRequest request)
    {
        // Can approve CTO requests
        if (request.Employee.JobTitle == JobTitle.CTO)
        {
            Console.WriteLine($"CEO approved vacation request for {request.Employee.Name}");
        }
        else
        {
            CallNext(request);
        }
    }
}
```

### 4. Request Object (`VacationRequest.cs`)
```csharp
internal class VacationRequest
{
    public Employee Employee { get; set; }
    public DateTime StartDate{ get; set; }
    public DateTime EndDate { get; set; }
    public double TotalDays => EndDate.Subtract(StartDate).TotalDays;
}
```

### 5. Usage Example (`Program.cs`)
```csharp
// Create a vacation request
var employee = new Employee
{
    Id = 1,
    Name = "John Doe",
    JobTitle = JobTitle.Developer,
};

var request = new VacationRequest
{
    Employee = employee,
    StartDate = DateTime.Today.AddDays(5),
    EndDate = DateTime.Today.AddDays(8), // 3 days
};

// Build the chain of responsibility
var teamLeaderHandler = new TeamLeaderApprovalHandler();
var technicalManagerHandler = new TechnicalManagerApprovalHandler();
var CTOHandler = new CTOApprovalHandler();
var CEOHandler = new CEOApprovalHandler();

// Link the chain
teamLeaderHandler.SetNextHandler(technicalManagerHandler);
technicalManagerHandler.SetNextHandler(CTOHandler);
CTOHandler.SetNextHandler(CEOHandler);

// Process the request (will stop at Team Leader since it's a 3-day Developer request)
teamLeaderHandler.Process(request);
```

## How the Chain Works

When a vacation request is submitted:

```
1. TeamLeaderHandler.Process()
   ├── Check: Is it a Developer request ≤ 3 days?
   ├── YES → Approve and stop
   └── NO → CallNext()

2. TechnicalManagerHandler.Process()
   ├── Check: Is it a Developer request > 3 days OR Team Leader request?
   ├── YES → Approve and stop
   └── NO → CallNext()

3. CTOHandler.Process()
   ├── Check: Is it a Technical Manager request?
   ├── YES → Approve and stop
   └── NO → CallNext()

4. CEOHandler.Process()
   ├── Check: Is it a CTO request?
   ├── YES → Approve and stop
   └── NO → CallNext() (No more handlers = rejection)
```

## Real-World Examples

- **Customer Support**: Basic → Advanced → Specialist → Manager
- **Exception Handling**: Try-catch blocks in nested scopes
- **Web Request Processing**: Authentication → Authorization → Validation → Business Logic
- **Expense Approval**: Employee → Manager → Director → VP → CFO
- **Loan Processing**: Teller → Loan Officer → Branch Manager → Regional Director

## Benefits in Our Implementation

1. **Flexible Hierarchy**: Easy to add new management levels or change approval rules
2. **Separation of Concerns**: Each handler only knows about its specific approval criteria
3. **Easy Testing**: Each handler can be tested independently
4. **Runtime Configuration**: Chain can be built dynamically based on business rules
5. **Fail-Safe**: Unhandled requests are caught and reported

## When to Use Chain of Responsibility

✅ **Use When:**
- You have multiple objects that can handle a request
- The handler isn't known in advance
- You want to decouple request sender from receiver
- You need flexible request processing workflow
- You want to add/remove handlers dynamically

❌ **Avoid When:**
- Only one object can handle the request
- You need guaranteed processing (every handler must process)
- Performance is critical (chain traversal adds overhead)
- The chain is very long and complex

## Variations

- **Multiple Handlers**: Allow multiple handlers to process the same request
- **Conditional Chaining**: Different chains based on request type
- **Bidirectional Chaining**: Handlers can send requests back up the chain
- **Priority-Based**: Handlers have priority levels for processing order

## Summary

The Chain of Responsibility pattern is like an organizational hierarchy where:

- **Each level** has specific authority and responsibility
- **Requests flow upward** until someone can handle them
- **No single point** knows the entire approval process
- **Easy to modify** approval workflows without changing existing code

In our vacation approval system, we can easily add new management levels, change approval limits, or modify the hierarchy without touching existing handlers. The pattern provides a clean, maintainable solution for complex approval workflows! 