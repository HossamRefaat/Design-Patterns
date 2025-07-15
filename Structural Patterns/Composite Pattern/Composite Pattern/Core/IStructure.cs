using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite_Pattern.Core
{
    public interface IStructure
    {
        public void Enter();
        public void Exit();
        public void Location();
        public string GetName();

    }
}
