using MediatorPattern.Core;

namespace MediatorPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Mediator Pattern: Client-Server Chat System ===\n");

            // Create the chat server (mediator)
            var chatServer = new ChatServer();

            // Add some initial clients for testing
            var alice = new ChatClient("Alice");
            var bob = new ChatClient("Bob");
            var charlie = new ChatClient("Charlie");

            chatServer.AddClient(alice);
            chatServer.AddClient(bob);
            chatServer.AddClient(charlie);

            Console.WriteLine("\nWelcome to the Interactive Chat System!");
            
            InteractiveMode(chatServer);
        }

        private static void InteractiveMode(ChatServer chatServer)
        {
            var currentUser = new ChatClient("You");
            chatServer.AddClient(currentUser);

            Console.WriteLine("\nAvailable commands:");
            Console.WriteLine("  broadcast <message>         - Send message to all clients");
            Console.WriteLine("  direct <name> <message>     - Send private message to specific client");
            Console.WriteLine("  history <name>              - Show chat history between you and another user");
            Console.WriteLine("  history-all                 - Show complete chat history");
            Console.WriteLine("  list                        - Show connected clients");
            Console.WriteLine("  help                        - Show this help message");
            Console.WriteLine("  quit                        - Exit chat system");
            Console.WriteLine();

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                    continue;

                var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var command = parts[0].ToLower();

                try
                {
                    switch (command)
                    {
                        case "broadcast":
                            if (parts.Length > 1)
                            {
                                var message = string.Join(" ", parts.Skip(1));
                                currentUser.SendBroadcastMessage(message);
                            }
                            else
                            {
                                Console.WriteLine("Usage: broadcast <message>");
                            }
                            break;

                        case "direct":
                            if (parts.Length > 2)
                            {
                                var recipient = parts[1];
                                var message = string.Join(" ", parts.Skip(2));
                                currentUser.SendDirectMessage(message, recipient);
                            }
                            else
                            {
                                Console.WriteLine("Usage: direct <name> <message>");
                            }
                            break;

                        case "history":
                            if (parts.Length > 1)
                            {
                                var otherUser = parts[1];
                                chatServer.ShowChatHistory(currentUser.Name, otherUser);
                            }
                            else
                            {
                                Console.WriteLine("Usage: history <name>");
                            }
                            break;

                        case "history-all":
                            chatServer.ShowAllChatHistory();
                            break;

                        case "list":
                            chatServer.ShowConnectedClients();
                            break;

                        case "help":
                            Console.WriteLine("\nAvailable commands:");
                            Console.WriteLine("  broadcast <message>         - Send message to all clients");
                            Console.WriteLine("  direct <name> <message>     - Send private message to specific client");
                            Console.WriteLine("  history <name>              - Show chat history between you and another user");
                            Console.WriteLine("  history-all                 - Show complete chat history");
                            Console.WriteLine("  list                        - Show connected clients");
                            Console.WriteLine("  help                        - Show this help message");
                            Console.WriteLine("  quit                        - Exit chat system");
                            break;

                        case "quit":
                            chatServer.RemoveClient(currentUser);
                            Console.WriteLine("Goodbye!");
                            return;

                        default:
                            Console.WriteLine("Unknown command. Type 'help' for available commands.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine();
            }
        }
    }
}
