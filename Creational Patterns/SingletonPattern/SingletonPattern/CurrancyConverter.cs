using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    public class CurrancyConverter
    {
        private IEnumerable<ExchangeRate> exchangeRates;

        private CurrancyConverter()
        {
            LoadExchangeRates();
        }

        private static object _lock = new();
        private static CurrancyConverter _instance;

        public static CurrancyConverter Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)    
                    {
                        if (_instance == null)
                        {
                            _instance = new();
                        }
                    }
                }
                return _instance;
            }
        }

        private void LoadExchangeRates()
        {
            Thread.Sleep(3000); // Simulate a delay for loading exchange rates
            exchangeRates = new[]
            {
                new ExchangeRate("USD", "EUR", 0.85m),
                new ExchangeRate("EUR", "USD", 1.18m),
                new ExchangeRate("USD", "GBP", 0.75m),
                new ExchangeRate("GBP", "USD", 1.33m)
            };
        }

        public decimal Convert(string baseCurrency, string targetCurrency, decimal amount)
        {
            var rate = exchangeRates
                .FirstOrDefault(e => e.BaseCurrancy == baseCurrency && e.TargetCurrancy == targetCurrency)?.Rate;
            return rate.HasValue ? amount * rate.Value : throw new InvalidOperationException("Exchange rate not found.");
        }
    }
}
