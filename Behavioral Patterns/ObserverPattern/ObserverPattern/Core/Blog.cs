using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern.Core
{
    /// <summary>
    /// Blog class - This is the SUBJECT in the Observer pattern
    /// It maintains a list of observers (subscribers) and notifies them when something changes
    /// </summary>
    public class Blog : ISubject
    {
        private readonly List<IObserver> _subscribers;
        public string Name { get; private set; }

        public Blog(string name)
        {
            Name = name;
            _subscribers = new List<IObserver>();
        }

        // Add an observer (subscriber) to the list
        public void Subscribe(IObserver observer)
        {
            _subscribers.Add(observer);
            Console.WriteLine($"{observer.Name} subscribed to {Name}");
        }

        // Remove an observer (subscriber) from the list
        public void Unsubscribe(IObserver observer)
        {
            _subscribers.Remove(observer);
            Console.WriteLine($"{observer.Name} unsubscribed from {Name}");
        }

        // Notify all subscribers about the new post
        public void NotifyObservers()
        {
            Console.WriteLine($"\n[{Name}] Notifying all subscribers...");
            foreach (var subscriber in _subscribers)
            {
                subscriber.Update(this);
            }
        }

        // Publish a new post and automatically notify subscribers
        public void PublishPost(string title, string author)
        {
            var post = new BlogPost(title, author);
            Console.WriteLine($"\n[{Name}] Published: {post}");
            
            // This is the key part - automatically notify all observers
            NotifyObservers();
        }

        public void ShowSubscribers()
        {
            Console.WriteLine($"\n{Name} has {_subscribers.Count} subscribers:");
            foreach (var subscriber in _subscribers)
            {
                Console.WriteLine($"- {subscriber.Name}");
            }
        }
    }
} 