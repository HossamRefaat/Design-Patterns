using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern.Core.ShoppingCarts
{
    internal class OnlineShoppingCart : ShoppingCart
    {
        protected override void ApplyDiscount(Invoice invoice)
        {
            if (invoice.TotalPrice > 10000)
                invoice.DiscountPercentage = 0.05;
        }
    }
}
