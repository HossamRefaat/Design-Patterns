using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    internal class SalaryCalculatorBuilder
    {
        private int taxPercentage = 0;
        private decimal bonusPercentage = 0;
        private decimal educationPackage = 0;
        private decimal transportation = 0;
        private bool sendPayslipToEmployee = true;
        private bool postResultsToGl = true;

        public SalaryCalculatorBuilder SetTaxPercentage(int taxPercentage)
        {
            LogMessage($"{taxPercentage}% taxes will be deducted.");
            this.taxPercentage = taxPercentage;
            return this;
        }

        public SalaryCalculatorBuilder SetBonusPercentage(decimal bonusPercentage)
        {
            LogMessage($"{bonusPercentage}% bonus will be added.");
            this.bonusPercentage = bonusPercentage;
            return this;
        }

        public SalaryCalculatorBuilder SetEducationPackage(decimal educationPackage)
        {
            LogMessage($"{educationPackage} EGP education package will be added.");
            this.educationPackage = educationPackage;
            return this;
        }

        public SalaryCalculatorBuilder SetTransportation(decimal transportation)
        {
            LogMessage($"{transportation} EGP transportation will be added.");
            this.transportation = transportation;
            return this;
        }

        public SalaryCalculatorBuilder SetSendPayslipToEmployee(bool sendPayslipToEmployee)
        {
            this.sendPayslipToEmployee = sendPayslipToEmployee;
            return this;
        }
        public SalaryCalculatorBuilder SetPostResultsToGl(bool postResultsToGl)
        {
            this.postResultsToGl = postResultsToGl;
            return this;
        }

        private void LogMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }   

        public SalaryCalculator Build()
        {
            LogMessage("Building Salary Calculator...");
            return new SalaryCalculator(taxPercentage, bonusPercentage, educationPackage, transportation, sendPayslipToEmployee, postResultsToGl);
        }
    }
}
