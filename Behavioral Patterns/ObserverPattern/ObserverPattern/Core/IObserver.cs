using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern.Core
{
    /// <summary>
    /// Observer interface - defines what observers (subscribers) must implement
    /// </summary>
    public interface IObserver
    {
        void Update(Blog blog);  // Called when the blog publishes new content
        string Name { get; }
    }
} 