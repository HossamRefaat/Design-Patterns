using FactoryMethodPattern.Payments.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern.Core.ShoppingCarts
{
    abstract class ShoppingCart
    {
        private List<InvoiceLine> lines = new List<InvoiceLine>();
        public void AddItem(int itemId, int quantity, double unitPrice)
        {
            lines.Add(new InvoiceLine { ItemId = itemId, Quantity = quantity, UnitPrice = unitPrice });
        }

        public void Checkout(Customer customer, PaymentProcessor paymentProcessor)
        {
            var invoice = new Invoice
            {
                Customer = customer,
                Lines = lines
            };

            ApplyTaxes(invoice);
            ApplyDiscount(invoice);
            ProcessPayment(invoice, paymentProcessor);
        }

        private void ProcessPayment(Invoice invoice, PaymentProcessor paymentProcessor)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"({GetType().Name}) Invoice created for customer `{invoice.Customer.Name}` with net price {invoice.NetPrice}");

            var payment = paymentProcessor.ProcessPayment(invoice.Customer.Id, invoice.NetPrice);
            Console.WriteLine($"Customer charged with {payment.ChargedAmount:0.00}, Payment ref: {payment.ReferenceNumber}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        abstract protected void ApplyDiscount(Invoice invoice);
        
        private void ApplyTaxes(Invoice invoice)
        {
            invoice.Taxes = invoice.TotalPrice * 0.15;
        }
    }
}
