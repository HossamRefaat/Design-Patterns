using ProxyPattern.Core;

namespace ProxyPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee employee1 = new Employee { Name = "Alice", SecurityLevel = 4 };
            Internet internet = new ProxyInternet();

            internet.GetInternetAccess(employee1); // Should deny access

            employee1.SecurityLevel = 6;

            internet.GetInternetAccess(employee1); // Should grant access
        }
    }
}
