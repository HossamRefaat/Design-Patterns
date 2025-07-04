using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern.Core
{
    public class PayrollSystemPayItemAdapter
    {
        private readonly PayItem payItem;

        public PayrollSystemPayItemAdapter(PayItem payItem)
        {
            this.payItem = payItem;
        }

        public string Name => payItem.Name;
        public decimal Value => payItem.IsDeduction ? -1 * payItem.Value : payItem.Value;
    }
}
