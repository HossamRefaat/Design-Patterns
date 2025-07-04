using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern.Core
{
    class PayrollSystemEmployeeAdapter
    {
        private readonly Employee employee;
        private readonly IEnumerable<PayrollSystemPayItemAdapter> payItems;

        public PayrollSystemEmployeeAdapter(Employee employee)
        {
            this.employee = employee;
            this.payItems = employee.PayItems.Select(pi => new PayrollSystemPayItemAdapter(pi)).ToList();
        }

        public string FullName => $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
        public IEnumerable<PayrollSystemPayItemAdapter> PayItems => payItems;
    }
}
