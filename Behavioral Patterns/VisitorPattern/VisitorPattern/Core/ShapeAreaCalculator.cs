using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern.Core
{
    internal class ShapeAreaCalculator : IShapeVisitor
    {
        public void Visit(Circle circle)
        {
            double area = Math.PI * circle.Radius * circle.Radius;
            Console.WriteLine($"Circle Area: {area:F2}");
        }

        public void Visit(Triangle triangle)
        {
            double area = 0.5 * triangle.Base * triangle.Height;
            Console.WriteLine($"Triangle Area: {area:F2}");
        }

        public void Visit(Rectangle recatangle)
        {
            double area = recatangle.Width * recatangle.Height;
            Console.WriteLine($"Rectangle Area: {area:F2}");
        }
    }
}
