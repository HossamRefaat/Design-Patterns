using StatePattern.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrders.Core.States
{
    public class DeliveredOrder : IOrderState
    {
        public void Next(Order order)
        {
            Console.WriteLine("Order already delivered.");
        }

        public void Cancel(Order order)
        {
            Console.WriteLine("Cannot cancel. Already delivered.");
        }

        public string GetStatus() => "Delivered";
    }

}
