using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern.Core
{
    /// <summary>
    /// The Mediator interface defines how components communicate with each other
    /// </summary>
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
} 