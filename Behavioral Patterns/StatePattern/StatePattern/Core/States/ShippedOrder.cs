using OnlineOrders.Core;
using StatePattern.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrders.Core.States
{
    public class ShippedOrder : IOrderState
    {
        public void Next(Order order)
        {
            Console.WriteLine("Order delivered.");
            order.SetState(new DeliveredOrder());
        }

        public void Cancel(Order order)
        {
            Console.WriteLine("Cannot cancel a shipped order.");
        }

        public string GetStatus() => "Shipped";
    }
}
