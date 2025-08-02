using ChainOfResponsibilityPattern.Core;
using ChainOfResponsibilityPattern.Core.Workflow;

/*
Team Leader can approve Developer requests up to 3 days otherwise It goes to Technical Manager
Technical Manager can approve Developer requests with more than 3 days and Team Leader requests as well
CTO can approve Technical Manager requests
CEO can approve CTO requests
*/

namespace ChainOfResponsibilityPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var employee = new Employee
            {
                Id = 1,
                Name = "John Doe",
                DateOfBirth = new DateOnly(1990, 1, 1),
                HireDate = new DateOnly(2020, 1, 1),
                JobTitle = JobTitle.Developer,
            };

            var request = new VacationRequest
            {
                Employee = employee,
                StartDate = DateTime.Today.AddDays(5),
                EndDate = DateTime.Today.AddDays(8),
            };

            var teamLeaderHandler = new TeamLeaderApprovalHandler();
            var technicalManagerHandler = new TechnicalManagerApprovalHandler();
            var CTOHandler = new CTOApprovalHandler();
            var CEOHandler = new CEOApprovalHandler();

            teamLeaderHandler.SetNextHandler(technicalManagerHandler);
            technicalManagerHandler.SetNextHandler(CTOHandler);
            CTOHandler.SetNextHandler(CEOHandler);

            teamLeaderHandler.Process(request);
        }
    }
}
