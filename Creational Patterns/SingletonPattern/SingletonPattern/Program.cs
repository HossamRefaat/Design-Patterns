namespace SingletonPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter base currancy");
                var baseCurrency = Console.ReadLine();
                Console.WriteLine("Enter target currancy");
                var targetCurrency = Console.ReadLine();
                Console.WriteLine("Enter amount to convert");
                var amount = decimal.Parse(Console.ReadLine());
                //var converter = new CurrancyConverter();

                var exchangedAmount = CurrancyConverter.Instance.Convert(baseCurrency, targetCurrency, amount);
                Console.WriteLine($"{amount} {baseCurrency} = {exchangedAmount} {targetCurrency}");
                Console.WriteLine("------------------------------------------");
            }
        }
    }
}
