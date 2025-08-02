using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern.Core.Workflow
{
    internal class TechnicalManagerApprovalHandler : ApprovalHandler
    {
        public override void Process(VacationRequest request)
        {
            //Technical Manager can approve Developer requests with more than 3 days and Team Leader requests as well
            if (request.Employee.JobTitle == JobTitle.Developer && request.TotalDays > 3)
            {
                Console.WriteLine($"Technical Manager approved vacation request for {request.Employee.Name} from {request.StartDate.ToShortDateString()} to {request.EndDate.ToShortDateString()}");
            }
            else if (request.Employee.JobTitle == JobTitle.TeamLeader)
            {
                Console.WriteLine($"Technical Manager approved vacation request for Team Leader {request.Employee.Name} from {request.StartDate.ToShortDateString()} to {request.EndDate.ToShortDateString()}");
            }
            else
            {
                CallNext(request);
            }
        }
    }
}
