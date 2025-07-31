using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern.Core
{
    public class RealInternetConnection : Internet
    {
        public void GetInternetAccess(Employee employee)
        {
            Console.WriteLine($"Granted Internet Permission for {employee.Name}");
        }
    }
}
