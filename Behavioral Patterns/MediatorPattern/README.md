# Mediator Design Pattern

## What is the Mediator Design Pattern?

The **Mediator Design Pattern** is a behavioral pattern that defines how a set of objects interact with each other. Instead of objects communicating directly, they communicate through a central mediator object. Think of it like an air traffic control tower - planes don't communicate directly with each other, they all communicate through the control tower which coordinates all interactions safely and efficiently.

## Key Benefits

- **Reduces coupling** - Objects don't need to know about each other directly
- **Centralizes communication logic** - All interaction rules are in one place
- **Promotes reusability** - Components can be reused in different contexts
- **Simplifies maintenance** - Changes to communication logic happen in one place
- **Enables complex interactions** - Mediator can implement sophisticated communication patterns
- **Single Responsibility** - Each component focuses on its core functionality

## Key Components

1. **Mediator Interface** (`IChatMediator`) - Defines the communication contract
2. **Concrete Mediator** (`ChatServer`) - Implements the communication logic and manages participants
3. **Colleague Interface** (`IClient`) - Defines how participants interact with the mediator
4. **Concrete Colleagues** (`ChatClient`) - Individual participants that communicate through the mediator
5. **Message Storage** (`ChatMessage`, `ChatHistory`) - Handles message persistence and history queries

## Our Implementation: Client-Server Chat System

This project demonstrates the Mediator pattern through a chat system where clients communicate through a central server. The server handles both broadcast messages (to all clients) and direct messages (to specific clients), with complete chat history functionality.

### Core Features
- **Broadcast Messaging**: Send messages to all connected clients
- **Direct Messaging**: Send private messages to specific clients
- **Chat History**: View conversation history between users
- **Client Management**: Add and remove clients dynamically
- **Error Handling**: Graceful handling of invalid recipients
- **Interactive Mode**: Real-time chat simulation

### 1. Mediator Interface (`IChatMediator.cs`)
```csharp
public interface IChatMediator
{
    void SendMessage(string message, IClient sender);
    void SendDirectMessage(string message, IClient sender, string recipientName);
    void AddClient(IClient client);
    void RemoveClient(IClient client);
    IEnumerable<IClient> GetClients();
    void ShowChatHistory(string user1, string user2);
    void ShowAllChatHistory();
}
```
This defines the contract for how the server manages all client communications and chat history.

### 2. Message Classes

#### Chat Message (`ChatMessage.cs`)
```csharp
public class ChatMessage
{
    public string Content { get; set; }
    public string SenderName { get; set; }
    public string RecipientName { get; set; } // null for broadcast messages
    public DateTime Timestamp { get; set; }
    public bool IsPrivate { get; set; }
}
```
Represents individual messages with metadata for history tracking.

#### Chat History (`ChatHistory.cs`)
```csharp
public class ChatHistory
{
    public void AddMessage(ChatMessage message);
    public IEnumerable<ChatMessage> GetConversationBetween(string user1, string user2);
    public IEnumerable<ChatMessage> GetAllMessages();
    public IEnumerable<ChatMessage> GetBroadcastMessages();
}
```
Manages message storage and provides querying capabilities.

### 3. Concrete Mediator (`ChatServer.cs`)
```csharp
public class ChatServer : IChatMediator
{
    private readonly List<IClient> _clients;
    private readonly ChatHistory _chatHistory;

    public void SendMessage(string message, IClient sender)
    {
        // Record the broadcast message
        var chatMessage = new ChatMessage(message, sender.Name);
        _chatHistory.AddMessage(chatMessage);

        // Broadcast to all clients except sender
        foreach (var client in _clients.Where(c => c != sender))
        {
            client.ReceiveMessage(message, sender.Name);
        }
    }

    public void SendDirectMessage(string message, IClient sender, string recipientName)
    {
        var recipient = _clients.FirstOrDefault(c => c.Name.Equals(recipientName, StringComparison.OrdinalIgnoreCase));
        
        if (recipient != null)
        {
            // Record the private message
            var chatMessage = new ChatMessage(message, sender.Name, recipient.Name);
            _chatHistory.AddMessage(chatMessage);
            
            recipient.ReceiveMessage($"[PRIVATE] {message}", sender.Name);
        }
        else
        {
            Console.WriteLine($"[SERVER] Client '{recipientName}' not found.");
        }
    }
}
```
**Responsibilities**:
- Maintains list of connected clients
- Routes broadcast messages to all clients except sender
- Routes direct messages to specific recipients
- Records all messages for history tracking
- Handles client connections and disconnections
- Provides error handling for invalid recipients
- Manages chat history display

### 4. Client Interface (`IClient.cs`)
```csharp
public interface IClient
{
    string Name { get; }
    void ReceiveMessage(string message, string senderName);
    void SendBroadcastMessage(string message);
    void SendDirectMessage(string message, string recipientName);
    void SetMediator(IChatMediator mediator);
}
```
This defines how clients interact with the chat system.

### 5. Concrete Client (`ChatClient.cs`)
```csharp
public class ChatClient : IClient
{
    private IChatMediator _mediator;
    public string Name { get; private set; }

    public void SendBroadcastMessage(string message)
    {
        Console.WriteLine($"[{Name}] Broadcasting: {message}");
        _mediator.SendMessage(message, this);
    }

    public void SendDirectMessage(string message, string recipientName)
    {
        Console.WriteLine($"[{Name}] Sending private message to {recipientName}: {message}");
        _mediator.SendDirectMessage(message, this, recipientName);
    }

    public void ReceiveMessage(string message, string senderName)
    {
        Console.WriteLine($"[{Name}] Received from {senderName}: {message}");
    }
}
```
**Features**:
- Sends messages through the mediator (never directly to other clients)
- Receives messages from the mediator
- Maintains reference to mediator for communication
- Provides user-friendly logging of all activities

### 6. Usage Examples

#### Basic Setup
```csharp
// Create the chat server (mediator)
var chatServer = new ChatServer();

// Create clients
var alice = new ChatClient("Alice");
var bob = new ChatClient("Bob");
var charlie = new ChatClient("Charlie");

// Connect clients to server
chatServer.AddClient(alice);
chatServer.AddClient(bob);
chatServer.AddClient(charlie);
```

#### Broadcast Messaging
```csharp
// Alice sends a message to everyone
alice.SendBroadcastMessage("Hello everyone!");

// Output:
// [Alice] Broadcasting: Hello everyone!
// [SERVER] Broadcasting message from Alice
// [Bob] Received from Alice: Hello everyone!
// [Charlie] Received from Alice: Hello everyone!
```

#### Direct Messaging
```csharp
// Alice sends a private message to Bob
alice.SendDirectMessage("Hey Bob, want to grab coffee?", "Bob");

// Output:
// [Alice] Sending private message to Bob: Hey Bob, want to grab coffee?
// [SERVER] Direct message from Alice to Bob
// [Bob] Received from Alice: [PRIVATE] Hey Bob, want to grab coffee?
```

#### Chat History
```csharp
// View conversation history between two users
chatServer.ShowChatHistory("Alice", "Bob");

// Output:
// [SERVER] Chat history between Alice and Bob:
// --------------------------------------------------
// 14:30:15 [PRIVATE] Alice to Bob: Hey Bob, want to grab coffee?
// 14:30:45 [PRIVATE] Bob to Alice: Sure! Let's meet at 3 PM.
// --------------------------------------------------
```

## Communication Flow

```
Client A ──────┐
               ├───→ Chat Server (Mediator) ───→ Client B
Client C ──────┘       ↓                    ───→ Client D
                  Chat History
                   Storage
```

### Broadcast Message Flow:
1. Client A calls `SendBroadcastMessage()`
2. Client A calls mediator's `SendMessage()`
3. Mediator records message in chat history
4. Mediator loops through all clients except sender
5. Mediator calls `ReceiveMessage()` on each recipient

### Direct Message Flow:
1. Client A calls mediator's `SendDirectMessage()` with recipient name
2. Mediator finds the recipient by name
3. Mediator records private message in chat history
4. Mediator calls `ReceiveMessage()` on the specific recipient

## Interactive Chat Features

The program provides an interactive chat system with the following commands:

```
Available commands:
  broadcast <message>         - Send message to all clients
  direct <name> <message>     - Send private message to specific client
  history <name>              - Show chat history between you and another user
  history-all                 - Show complete chat history
  list                        - Show connected clients
  help                        - Show this help message
  quit                        - Exit chat system
```

### Example Interactive Session:
```
> list
[SERVER] Connected clients (4):
  - Alice
  - Bob
  - Charlie
  - You

> direct Alice How are you doing?
[You] Sending private message to Alice: How are you doing?
[SERVER] Direct message from You to Alice
[Alice] Received from You: [PRIVATE] How are you doing?

> broadcast Hello everyone!
[You] Broadcasting: Hello everyone!
[SERVER] Broadcasting message from You
[Alice] Received from You: Hello everyone!
[Bob] Received from You: Hello everyone!
[Charlie] Received from You: Hello everyone!

> history Alice
[SERVER] Chat history between You and Alice:
--------------------------------------------------
14:30:15 [PRIVATE] You to Alice: How are you doing?
--------------------------------------------------

> history-all
[SERVER] Complete chat history:
--------------------------------------------------
14:30:15 [PRIVATE] You to Alice: How are you doing?
14:30:30 [BROADCAST] You to ALL: Hello everyone!
--------------------------------------------------
```
## Real-World Examples

- **Chat Applications**: WhatsApp, Discord, Slack - central server mediates all messages
- **Air Traffic Control**: Tower coordinates all plane communications
- **GUI Frameworks**: Event systems where UI components communicate through event managers
- **Workflow Engines**: Task coordination through central workflow manager
- **Database Connection Pools**: Applications communicate with databases through pool manager
- **Message Queues**: Systems like RabbitMQ, Apache Kafka mediate message passing

## When to Use Mediator Pattern

✅ **Use When:**
- Objects communicate in complex ways with many interconnections
- Reusing objects is difficult due to tight coupling
- Behavior distributed among many classes needs to be customized
- You want to centralize complex communications and control logic

❌ **Avoid When:**
- Simple one-to-one or one-to-many communications are sufficient
- The mediator becomes overly complex (God Object anti-pattern)
- Performance is critical (mediator adds overhead)
- Direct communication is more efficient and maintainable

## Summary

The Mediator Pattern acts as a **communication hub** that:

- **Centralizes interaction logic** in one place
- **Reduces dependencies** between communicating objects
- **Simplifies maintenance** by localizing changes
- **Enables complex workflows** through coordinated interactions
- **Promotes loose coupling** while maintaining functionality
- **Provides message persistence** for chat history functionality

In our chat system, the server mediator allows clients to communicate without knowing about each other's existence. Clients only know how to talk to the server, and the server handles all the complexity of routing messages, managing connections, handling errors, and maintaining chat history.

This makes the system highly maintainable and extensible - we can easily add features like message encryption, user authentication, group channels, or message filtering by only modifying the mediator, without changing any client code! 