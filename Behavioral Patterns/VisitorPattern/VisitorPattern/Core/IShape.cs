using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern.Core
{
    internal interface IShape
    {
        void Accept(IShapeVisitor visitor);
    }
}
