using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern.Core.ShoppingCarts
{
    class InStoreShoppingCart : ShoppingCart
    {
        protected override void ApplyDiscount(Invoice invoice)
        {
            
        }
    }
}
