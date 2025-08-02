using StatePattern.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrders.Core.States
{
    public class CancelledOrder : IOrderState
    {
        public void Next(Order order)
        {
            Console.WriteLine("Cannot move forward. Order is cancelled.");
        }

        public void Cancel(Order order)
        {
            Console.WriteLine("Already cancelled.");
        }

        public string GetStatus() => "Cancelled";
    }
}
