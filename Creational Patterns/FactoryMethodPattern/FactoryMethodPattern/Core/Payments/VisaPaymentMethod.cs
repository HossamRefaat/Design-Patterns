using FactoryMethodPattern.Payments.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern.Core.Payments
{
    class VisaPaymentMethod : IPaymentMethod
    {
        public Payment Charge(int customerId, double amount)
        {
            return new Payment
            {
                CustomerId = customerId,
                ChargedAmount = amount + (amount < 10000 ? amount * 0.02 : 0),
                ReferenceNumber = Guid.NewGuid()
            };
        }
    }
}
