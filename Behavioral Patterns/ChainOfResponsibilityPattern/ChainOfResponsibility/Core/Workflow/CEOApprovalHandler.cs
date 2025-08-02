using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern.Core.Workflow
{
    internal class CEOApprovalHandler : ApprovalHandler
    {
        public override void Process(VacationRequest request)
        {
            //CEO can approve CTO requests
            if (request.Employee.JobTitle == JobTitle.CTO)
            {
                Console.WriteLine($"CEO approved vacation request for {request.Employee.Name} from {request.StartDate.ToShortDateString()} to {request.EndDate.ToShortDateString()}");
            }
            else
            {
                CallNext(request);
            }
        }
    }
}

