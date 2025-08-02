using OnlineOrders.Core.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePattern.Core
{
    public class Order
    {
        private IOrderState _state;

        public Order()
        {
            _state = new NewOrder(); // default state
        }

        public void SetState(IOrderState state)
        {
            _state = state;
        }

        public void NextStep() => _state.Next(this);
        public void CancelOrder() => _state.Cancel(this);
        public string GetStatus() => _state.GetStatus();
    }

}
