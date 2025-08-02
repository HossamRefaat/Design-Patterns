using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern.Core
{
    internal class Employee
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateOnly HireDate { get; set; }
        public JobTitle JobTitle { get; set; }
        public bool IsTerminated { get; set; }
    }
}
