using FacadePattern.Core.DiscountStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePattern.Core
{
    class InvoiceManager
    {
        private ICustomerDiscountStrategy _customerDiscountStrategy;

        public void SetDiscountStrategy(ICustomerDiscountStrategy strategy)
        {
            _customerDiscountStrategy = strategy;
        }

        public Invoice CreateInvoice(Customer customer, int quantity, int unitPrice)
        {
            var invoice = new Invoice
            {
                Customer = customer,
                Lines = new List<InvoiceLine>
                {
                    new InvoiceLine
                    {
                        Quantity = quantity,
                        UnitPrice = unitPrice
                    }
                },
            };
            invoice.DiscountPercentage = _customerDiscountStrategy.CalculateDiscount(invoice.TotalPrice);
            return invoice;
        }
    }
}
