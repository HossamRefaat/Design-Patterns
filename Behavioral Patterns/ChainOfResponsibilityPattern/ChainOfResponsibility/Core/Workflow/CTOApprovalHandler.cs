using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern.Core.Workflow
{
    internal class CTOApprovalHandler : ApprovalHandler
    {
        public override void Process(VacationRequest request)
        {
            //CTO can approve Technical Manager requests
            if(request.Employee.JobTitle == JobTitle.TechnicalManager)
            {
                Console.WriteLine($"CTO approved vacation request for {request.Employee.Name} from {request.StartDate.ToShortDateString()} to {request.EndDate.ToShortDateString()}");
            }
            else
            {
                CallNext(request);
            }
        }
    }
}
