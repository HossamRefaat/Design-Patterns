using FactoryMethodPattern.Payments.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern.Core.Payments
{
    class PayPalPaymentMethod : IPaymentMethod
    {
        public Payment Charge(int customerId, double amount)
        {
            return new Payment
            {
                CustomerId = customerId,
                ChargedAmount = amount + amount * 0.05,
                ReferenceNumber = Guid.NewGuid()
            };
        }
    }
}
