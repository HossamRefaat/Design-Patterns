using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern.Core
{
    /// <summary>
    /// Concrete Mediator - The Chat Server that handles all communication between clients
    /// </summary>
    public class ChatServer : IChatMediator
    {
        private readonly List<IClient> _clients;
        private readonly ChatHistory _chatHistory;

        public ChatServer()
        {
            _clients = new List<IClient>();
            _chatHistory = new ChatHistory();
        }

        public void AddClient(IClient client)
        {
            _clients.Add(client);
            client.SetMediator(this);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SERVER] {client.Name} joined the chat. Total clients: {_clients.Count}");
            Console.ResetColor();
        }

        public void RemoveClient(IClient client)
        {
            _clients.Remove(client);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[SERVER] {client.Name} left the chat. Total clients: {_clients.Count}");
            Console.ResetColor();
        }

        public void SendMessage(string message, IClient sender)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[SERVER] Broadcasting message from {sender.Name}");
            Console.ResetColor();

            // Record the broadcast message
            var chatMessage = new ChatMessage(message, sender.Name);
            _chatHistory.AddMessage(chatMessage);
             
            // Send to all clients except the sender
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
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[SERVER] Direct message from {sender.Name} to {recipient.Name}");
                Console.ResetColor();
                
                // Record the private message
                var chatMessage = new ChatMessage(message, sender.Name, recipient.Name);
                _chatHistory.AddMessage(chatMessage);
                
                recipient.ReceiveMessage($"[PRIVATE] {message}", sender.Name);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[SERVER] Client '{recipientName}' not found. Message not delivered.");
                Console.ResetColor();
            }
        }

        public IEnumerable<IClient> GetClients()
        {
            return _clients.AsReadOnly();
        }

        public void ShowConnectedClients()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[SERVER] Connected clients ({_clients.Count}):");
            foreach (var client in _clients)
            {
                Console.WriteLine($"  - {client.Name}");
            }
            Console.ResetColor();
        }

        public void ShowChatHistory(string user1, string user2)
        {
            var conversation = _chatHistory.GetConversationBetween(user1, user2);
            
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n[SERVER] Chat history between {user1} and {user2}:");
            Console.WriteLine(new string('-', 50));
            Console.ResetColor();

            if (!conversation.Any())
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("No private messages found between these users.");
                Console.ResetColor();
            }
            else
            {
                foreach (var message in conversation)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(message.ToString());
                    Console.ResetColor();
                }
            }
            Console.WriteLine(new string('-', 50));
        }

        public void ShowAllChatHistory()
        {
            var allMessages = _chatHistory.GetAllMessages();
            
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n[SERVER] Complete chat history:");
            Console.WriteLine(new string('-', 50));
            Console.ResetColor();

            if (!allMessages.Any())
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("No messages in chat history.");
                Console.ResetColor();
            }
            else
            {
                foreach (var message in allMessages)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(message.ToString());
                    Console.ResetColor();
                }
            }
            Console.WriteLine(new string('-', 50));
        }
    }
} 