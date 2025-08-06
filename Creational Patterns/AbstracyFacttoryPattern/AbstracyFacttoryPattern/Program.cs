using AbstracyFacttoryPattern.Core;
using AbstracyFacttoryPattern.Core.ConcreteFactories;

namespace AbstracyFacttoryPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IGUIFactory factory = new WindowsFactory();

            var app = new Application(factory);
            app.Run();
        }
    }
}