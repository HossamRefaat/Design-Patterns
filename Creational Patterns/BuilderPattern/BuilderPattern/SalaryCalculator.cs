using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    internal class SalaryCalculator
    {
        public SalaryCalculator(int taxPercentage = 0, decimal bonusPercentage = 0, decimal educationPackage = 0,
            decimal transportation = 0, bool sendPayslipToEmployee = true, bool postResultsToGl = true)
        {
            TaxPercentage = taxPercentage;
            BonusPercentage = bonusPercentage;
            EducationPackage = educationPackage;
            Transportation = transportation;
            SendPayslipToEmployee = sendPayslipToEmployee;
            PostResultsToGl = postResultsToGl;
        }

        public int TaxPercentage { get; }
        public decimal BonusPercentage { get; }
        public decimal EducationPackage { get; }
        public decimal Transportation { get; }
        public bool SendPayslipToEmployee { get; }
        public bool PostResultsToGl { get; }

        public decimal CalculateSalary(Employee employee)
        {
            if (employee is null)
            {
                throw new NullReferenceException("Cannot be null");
            }

            decimal taxAmount = employee.BasicSalary * TaxPercentage / 100;
            decimal bonusAmount = employee.BasicSalary * BonusPercentage / 100;
            decimal totalSalary = employee.BasicSalary - taxAmount + bonusAmount + EducationPackage + Transportation;

            Console.ForegroundColor = ConsoleColor.Green;

            if (SendPayslipToEmployee)
            {
                Console.WriteLine($"Payslip sent to '{employee.Email}'");
            }
            if (PostResultsToGl)
            {
                Console.WriteLine($"Salary voucher with total amount ({totalSalary} EGP) has been sent to GL");
            }
            Console.ResetColor();
            return totalSalary;
        }
    }
}
