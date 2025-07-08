using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern.Core
{
    internal class CustomerDataReader
    {
        public List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "Alice" , Category = CustomerCategory.Gold},
                new Customer { Id = 2, Name = "Bob", Category = CustomerCategory.Silver },
                new Customer { Id = 3, Name = "Lord Voldomort", Category = CustomerCategory.None }
            };
        }
    }
}
