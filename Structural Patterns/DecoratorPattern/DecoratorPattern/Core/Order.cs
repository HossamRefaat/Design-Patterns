using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Core
{
    internal class Order
    {
        private List<OrderLine> _lines = new List<OrderLine>();

        public IEnumerable<OrderLine> Lines => _lines.AsReadOnly();

        public void AddLine(int itemId, double quantity, decimal unitPrice)
        {
            _lines.Add(new OrderLine { ItemId = itemId, Quantity = quantity, UnitPrice = unitPrice });
        }   
    }
}
