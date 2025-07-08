using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethodPattern.Core.DiscountStrategies
{
    public interface ICustomerDiscountStrategy
    {
        double CalculateDiscount(double totalPrice);
    }
}
