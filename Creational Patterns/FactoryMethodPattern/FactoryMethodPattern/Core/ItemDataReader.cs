using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern.Core
{
    internal class ItemDataReader
    {
        public List<Item> GetItems()
        {
            return new List<Item>
            {
                new Item { Id = 1, Name = "Laptop" , UnitPrice = 10000.0},
                new Item { Id = 2, Name = "LCD" , UnitPrice = 2000.0},
                new Item { Id = 3, Name = "Keyboard", UnitPrice = 150.0 },
                new Item { Id = 4, Name = "Mouse", UnitPrice = 100.0 }
            };
        }
    }
}
