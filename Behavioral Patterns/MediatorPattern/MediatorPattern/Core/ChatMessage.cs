using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern.Core
{
    /// <summary>
    /// Represents a chat message with metadata
    /// </summary>
    public class ChatMessage
    {
        public string Content { get; set; }
        public string SenderName { get; set; }
        public string RecipientName { get; set; } // null for broadcast messages
        public DateTime Timestamp { get; set; }
        public bool IsPrivate { get; set; }

        public ChatMessage(string content, string senderName, string recipientName = null)
        {
            Content = content;
            SenderName = senderName;
            RecipientName = recipientName;
            Timestamp = DateTime.Now;
            IsPrivate = !string.IsNullOrEmpty(recipientName);
        }

        public override string ToString()
        {
            var messageType = IsPrivate ? "[PRIVATE]" : "[BROADCAST]";
            var recipient = IsPrivate ? $" to {RecipientName}" : " to ALL";
            return $"{Timestamp:HH:mm:ss} {messageType} {SenderName}{recipient}: {Content}";
        }
    }
} 