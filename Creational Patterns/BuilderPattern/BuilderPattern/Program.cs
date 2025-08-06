namespace BuilderPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new SalaryCalculatorBuilder();
            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add 20% Bouns");
                Console.WriteLine("2. Deduct 10% Taxes");
                Console.WriteLine("3. Add 2000 Eduction Package");
                Console.WriteLine("4. Add 1000 Transportaion");
                Console.WriteLine("5. Send Payslip to Employee");
                Console.WriteLine("6. Post Voucher to GL");
                Console.WriteLine("0. Build");

                var option = int.Parse(Console.ReadLine());
                if (option == 1)
                    builder.SetBonusPercentage(20);
                else if (option == 2)
                    builder.SetTaxPercentage(20);
                else if (option == 3)
                    builder.SetEducationPackage(2000);
                else if (option == 4)
                    builder.SetTransportation(1000);
                else if (option == 5)
                    builder.SetSendPayslipToEmployee(true);
                else if(option == 6)
                    builder.SetPostResultsToGl(true);
                else
                {
                    var calculator = builder.Build();
                    var employee = new Employee("Hossam mazmaz", "mazmaz@example.com", 20000);
                    var salary = calculator.CalculateSalary(employee);
                    Console.ReadKey();
                    builder = new SalaryCalculatorBuilder();
                }
            }
        }
    }
}
