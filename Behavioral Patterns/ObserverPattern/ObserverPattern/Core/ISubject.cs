using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern.Core
{
    /// <summary>
    /// The Subject interface defines methods for managing observers
    /// </summary>
    public interface ISubject
    {
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        void NotifyObservers();
    }
} 