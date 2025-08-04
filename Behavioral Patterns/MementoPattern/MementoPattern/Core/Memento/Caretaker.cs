using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoPattern.Core.Memento
{
    internal class Caretaker
    {
        private List<OrderMemento> _mementos = new();

        public int AddMemento(OrderMemento memento)
        {
            _mementos.Add(memento);
            return _mementos.Count - 1;
        }

        public OrderMemento GetMemento(int index)
        {
            if (index < 0 || index >= _mementos.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid memento index.");
            }
            return _mementos[index];
        }
    }
}