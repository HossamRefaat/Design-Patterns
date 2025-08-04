using CommandPattern.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoPattern.Core.Memento
{
    internal class OrderMemento
    {
        private readonly IEnumerable<OrderLine> lines;

        public OrderMemento(IEnumerable<OrderLine> lines)
        {
            this.lines = lines;
        }

        public IEnumerable<OrderLine> GetLines() => lines;
    }
}