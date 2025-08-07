using FlyweightPattern.Core;

namespace FlyweightPattern
{
    internal class Program
    {
        static void Main()
        {
            PlayerFactory factory = new();

            var terrorist = factory.GetPlayer("Terrorist");
            terrorist.AssignMission("Plant the bomb");
            terrorist.Display(10, 20);

            var counter = factory.GetPlayer("CounterTerrorist");
            counter.AssignMission("Defuse the bomb");
            counter.Display(5, 15);

            // Reuse existing instance
            var anotherTerrorist = factory.GetPlayer("Terrorist");
            anotherTerrorist.Display(30, 40);
        }
    }
}
