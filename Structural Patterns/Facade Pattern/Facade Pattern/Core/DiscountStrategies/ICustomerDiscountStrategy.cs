using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePattern.Core.DiscountStrategies
{
    public interface ICustomerDiscountStrategy
    {
        double CalculateDiscount(double totalPrice);
    }
}
