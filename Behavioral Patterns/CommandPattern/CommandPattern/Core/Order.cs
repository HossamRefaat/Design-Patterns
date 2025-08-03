using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Core
{
    internal class Order
    {
        public int Id { get; } = Random.Shared.Next(1, 1000);

        private List<OrderLine> _lines = new();
        public IEnumerable<OrderLine> Lines => _lines.AsReadOnly();

        public void AddProduct(Product product, double quantity)
        {
            _lines.Add(new OrderLine { ProductId = product.Id, UnitPrice = product.UnitPrice, Quantity = quantity });

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Product `{product.Name}` added, Order now contains {_lines.Count} products");
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal void RemoveProductAt(int index)
        {
            _lines.RemoveAt(index);
        }
    }
}
