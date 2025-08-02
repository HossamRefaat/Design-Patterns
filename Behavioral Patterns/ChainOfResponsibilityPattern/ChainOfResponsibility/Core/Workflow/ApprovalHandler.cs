using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern.Core.Workflow
{
    internal abstract class ApprovalHandler : IApprovalHandler
    {
        private IApprovalHandler _nextHandler;
        public abstract void Process(VacationRequest request);
    
        public void SetNextHandler(IApprovalHandler handler)
        {
            _nextHandler = handler;
        }

        protected void CallNext(VacationRequest request)
        {
            if (_nextHandler != null)
            {
                _nextHandler.Process(request);
            }
            else
            {
                Console.WriteLine($"No handler found for request from {request.Employee.Name} for {request.TotalDays} days.");
            }
        }
    }
}
