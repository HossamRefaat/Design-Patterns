using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    public class ExchangeRate
    {
        public ExchangeRate(string baseCurrancy, string targetCurrancy, decimal rate)
        {
            BaseCurrancy = baseCurrancy;
            TargetCurrancy = targetCurrancy;
            Rate = rate;
        }

        public string BaseCurrancy { get; }
        public string TargetCurrancy { get; }
        public decimal Rate { get; }
    }
}
