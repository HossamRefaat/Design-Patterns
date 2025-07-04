using System.Diagnostics;

namespace AdapterPattern.Core
{
    public class EmployeeDataReader
    {
        public IEnumerable<Employee> GetEmployees()
        {
            // Simulate reading from a database
            return new List<Employee>
            {
                new Employee
                {
                    FirstName = "John",
                    SecondName = "A.",
                    LastName = "Doe",
                    PayItems = new List<PayItem>
                    {
                        new PayItem { Name = "Base Salary", Value = 3000 },
                        new PayItem { Name = "Bonus", Value = 500 },
                        new PayItem { Name = "Medical Insurance", Value = 200, IsDeduction = true }
                    }
                },
                new Employee
                {
                    FirstName = "Jane",
                    SecondName = "B.",
                    LastName = "Smith",
                     PayItems = new List<PayItem>
                    {
                        new PayItem { Name = "Base Salary", Value = 3000 },
                        new PayItem { Name = "Bonus", Value = 500 },
                        new PayItem { Name = "Medical Insurance", Value = -200 }
                    }
                }
            };
        }
    }
}
