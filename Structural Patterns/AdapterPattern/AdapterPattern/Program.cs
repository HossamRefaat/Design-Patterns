using AdapterPattern.Core;
using Microsoft.CSharp.RuntimeBinder;
using System.Text;
using System.Text.Json;

namespace AdapterPattern
{
    internal class Program
    {
        static async Task Run()
        {
            var payrollCalculatorUrl = "https://localhost:7143/api/PayrollCalculator";
            var reader = new EmployeeDataReader();
            var employees = reader.GetEmployees();

            var client = new HttpClient();
            foreach (var employee in employees)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, payrollCalculatorUrl);
                var employeeAdapter = new PayrollSystemEmployeeAdapter(employee);
                request.Content = new StringContent(
                    JsonSerializer.Serialize(employeeAdapter),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await client.SendAsync(request);
                var responseJson = await response.Content.ReadAsStringAsync();
                var salary = decimal.Parse(responseJson);

                Console.WriteLine($"Salary for employee: {employeeAdapter.FullName}, as of today: {salary}");
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        static async Task Main(string[] args)
        {
            await Run();
        }
    }
}
