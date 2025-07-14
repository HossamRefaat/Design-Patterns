using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePattern.Core.ShoppingCarts
{
    abstract class ShoppingCart
    {
        private List<InvoiceLine> lines = new List<InvoiceLine>();
        public void AddItem(int itemId, int quantity, int unitPrice)
        {
            lines.Add(new InvoiceLine { ItemId = itemId, Quantity = quantity, UnitPrice = unitPrice });
        }

        public void Checkout(Customer customer)
        {
            var invoice = new Invoice
            {
                Customer = customer,
                Lines = lines
            };

            ApplyTaxes(invoice);
            ApplyDiscount(invoice);
            ProcessPayment(invoice);
        }

        private void ProcessPayment(Invoice invoice)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"({GetType().Name}) Invoice created for customer `{invoice.Customer.Name}` with net price {invoice.NetPrice}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        abstract protected void ApplyDiscount(Invoice invoice);
        
        private void ApplyTaxes(Invoice invoice)
        {
            invoice.Taxes = invoice.TotalPrice * 0.15;
        }
    }
}
