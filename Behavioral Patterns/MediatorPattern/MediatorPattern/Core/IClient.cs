using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern.Core
{
    /// <summary>
    /// The Client interface defines how clients interact with the mediator
    /// </summary>
    public interface IClient
    {
        string Name { get; }
        void ReceiveMessage(string message, string senderName);
        void SendBroadcastMessage(string message);
        void SendDirectMessage(string message, string recipientName);
        void SetMediator(IChatMediator mediator);
    }
} 