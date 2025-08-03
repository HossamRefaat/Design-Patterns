using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern.Core
{
    /// <summary>
    /// Manages chat history and provides querying capabilities
    /// </summary>
    public class ChatHistory
    {
        private readonly List<ChatMessage> _messages;

        public ChatHistory()
        {
            _messages = new List<ChatMessage>();
        }

        public void AddMessage(ChatMessage message)
        {
            _messages.Add(message);
        }

        public IEnumerable<ChatMessage> GetConversationBetween(string user1, string user2)
        {
            return _messages.Where(m => 
                (m.SenderName.Equals(user1, StringComparison.OrdinalIgnoreCase) && 
                 m.RecipientName?.Equals(user2, StringComparison.OrdinalIgnoreCase) == true) ||
                (m.SenderName.Equals(user2, StringComparison.OrdinalIgnoreCase) && 
                 m.RecipientName?.Equals(user1, StringComparison.OrdinalIgnoreCase) == true))
                .OrderBy(m => m.Timestamp);
        }

        public IEnumerable<ChatMessage> GetAllMessages()
        {
            return _messages.OrderBy(m => m.Timestamp);
        }
    }
} 