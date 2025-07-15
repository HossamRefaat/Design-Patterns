using Composite_Pattern.Core;

namespace Composite_Pattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Housing myHouse = new Housing("123 Main St");
            Housing floor1 = new Housing("123 Main St - First Floor");

            int firstFloor = myHouse.AddStructure(floor1);

            Room washRoom1m = new Room("1F Men's Washroom");
            Room washRoom1w = new Room("1F Womne's Washroom");
            Room common1 = new Room("Comman Area");

            int firstMens = floor1.AddStructure(washRoom1m);
            int firstWomens = floor1.AddStructure(washRoom1w);
            int firstComman = floor1.AddStructure(common1);

            myHouse.Enter();

            Housing currentFloor = (Housing) myHouse.GetStructure(firstFloor);
            currentFloor.Enter();

            Room currentRoom = (Room) currentFloor.GetStructure(firstMens);
            currentRoom.Enter();

            currentRoom = (Room) currentFloor.GetStructure(firstComman);
            currentRoom.Enter();

            myHouse.Exit();
        }
    }
}
