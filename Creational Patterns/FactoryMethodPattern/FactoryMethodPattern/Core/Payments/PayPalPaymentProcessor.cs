using FactoryMethodPattern.Payments.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern.Core.Payments
{
    public class PayPalPaymentProcessor : PaymentProcessor
    {
        protected override IPaymentMethod CreatePaymentMethod()
        {
            return new PayPalPaymentMethod();
        }
    }
}
