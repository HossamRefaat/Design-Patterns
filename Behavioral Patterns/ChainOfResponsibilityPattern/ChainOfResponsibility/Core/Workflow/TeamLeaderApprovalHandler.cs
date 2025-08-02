using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern.Core.Workflow
{
    internal class TeamLeaderApprovalHandler : ApprovalHandler
    {
        public override void Process(VacationRequest request)
        {
            //Team Leader can approve Developer requests up to 3 days otherwise It goes to Technical Manager
            if (request.Employee.JobTitle == JobTitle.Developer && request.TotalDays <= 3)
            {
                Console.WriteLine($"Team Leader approved vacation request for {request.Employee.Name} from {request.StartDate.ToShortDateString()} to {request.EndDate.ToShortDateString()}");
            }
            else
            {
                CallNext(request);
            }
        }
    }
}
