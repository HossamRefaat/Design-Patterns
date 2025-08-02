using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePattern.Core
{
    public interface IOrderState
    {
        void Next(Order order);
        void Cancel(Order order);
        string GetStatus();
    }

}
