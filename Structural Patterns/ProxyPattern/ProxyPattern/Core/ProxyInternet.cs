using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern.Core
{
    internal class ProxyInternet : Internet
    {
        public void GetInternetAccess(Employee employee)
        {
            if(employee.SecurityLevel > 5)
            {
               new RealInternetConnection().GetInternetAccess(employee);
            }
            else
            {
                Console.WriteLine("Access Denied: Insufficient Security Level");
            }
        }
    }
}
