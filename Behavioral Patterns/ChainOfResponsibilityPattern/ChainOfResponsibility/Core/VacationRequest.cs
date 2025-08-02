using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern.Core
{
    internal class VacationRequest
    {
        public Employee Employee { get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }
        public double TotalDays => EndDate.Subtract(StartDate).TotalDays;
    }
}
