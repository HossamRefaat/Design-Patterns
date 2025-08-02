using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern.Core.Workflow
{
    internal interface IApprovalHandler
    {
        void SetNextHandler(IApprovalHandler handler);
        void Process(VacationRequest request);   
    }
}
