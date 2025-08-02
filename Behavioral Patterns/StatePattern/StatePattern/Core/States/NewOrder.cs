using OnlineOrders.Core;
using StatePattern.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrders.Core.States
{
    internal class NewOrder : IOrderState
    {
        public void Next(Order order)
        {
            Console.WriteLine("Order moved to Processing.");
            order.SetState(new ProcessingOrder());
        }

        public void Cancel(Order order)
        {
            Console.WriteLine("Order cancelled.");
            order.SetState(new CancelledOrder());
        }

        public string GetStatus() => "New";
    }
}
