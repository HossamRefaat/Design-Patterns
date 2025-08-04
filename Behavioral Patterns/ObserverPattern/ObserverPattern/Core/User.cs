using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern.Core
{
    /// <summary>
    /// User class - This is the OBSERVER in the Observer pattern
    /// It receives notifications when blogs it's subscribed to publish new posts
    /// </summary>
    public class User : IObserver
    {
        public string Name { get; private set; }

        public User(string name)
        {
            Name = name;
        }

        // This method is called by the blog when it publishes new content
        public void Update(Blog blog)
        {
            Console.WriteLine($"   → {Name} received notification from {blog.Name}!");
        }

        // Helper method to subscribe to a blog
        public void SubscribeTo(Blog blog)
        {
            blog.Subscribe(this);
        }

        // Helper method to unsubscribe from a blog
        public void UnsubscribeFrom(Blog blog)
        {
            blog.Unsubscribe(this);
        }
    }
} 