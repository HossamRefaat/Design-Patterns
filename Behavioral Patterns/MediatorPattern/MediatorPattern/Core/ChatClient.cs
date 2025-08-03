using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern.Core
{
    /// <summary>
    /// Concrete Client - Represents a chat participant
    /// </summary>
    public class ChatClient : IClient
    {
        private IChatMediator _mediator;

        public string Name { get; private set; }

        public ChatClient(string name)
        {
            Name = name;
        }

        public void SetMediator(IChatMediator mediator)
        {
            _mediator = mediator;
        }

        public void ReceiveMessage(string message, string senderName)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[{Name}] Received from {senderName}: {message}");
            Console.ResetColor();
        }

        public void SendBroadcastMessage(string message)
        {
            if (_mediator == null)
            {
                Console.WriteLine($"[{Name}] Error: Not connected to server");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[{Name}] Broadcasting: {message}");
            Console.ResetColor();

            _mediator.SendMessage(message, this);
        }

        public void SendDirectMessage(string message, string recipientName)
        {
            if (_mediator == null)
            {
                Console.WriteLine($"[{Name}] Error: Not connected to server");
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"[{Name}] Sending private message to {recipientName}: {message}");
            Console.ResetColor();

            _mediator.SendDirectMessage(message, this, recipientName);
        }

        public override string ToString()
        {
            return Name;
        }
    }
} 