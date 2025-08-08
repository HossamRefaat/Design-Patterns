using VisitorPattern.Core;

namespace VisitorPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<IShape> shapes = new List<IShape>
            {   
                new Circle(3),
                new Rectangle(4, 5),
                new Triangle(6, 2)
            };

            var areaVisitor = new ShapeAreaCalculator();

            foreach (var shape in shapes)
            {
                shape.Accept(areaVisitor);
            }
        }
    }
}
