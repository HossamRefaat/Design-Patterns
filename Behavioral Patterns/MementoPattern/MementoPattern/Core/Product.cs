using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Core
{
    internal class Product
    {
        public Product(int id, string name, double unitPrice, double stockBalance)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            StockBalance = stockBalance;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public double UnitPrice { get; private set; }
        public double StockBalance { get; private set; }

        public void AddStock(double quantity)
        {
            StockBalance += quantity;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Product `{Name}` stock changed to {StockBalance}");
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
