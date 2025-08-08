using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern.Core
{
    internal interface IShapeVisitor
    {
        void Visit(Circle circle);
        void Visit(Rectangle recatangle);
        void Visit(Triangle triangle);  
    }
}
