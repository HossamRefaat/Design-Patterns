using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweightPattern.Core
{
    internal class Terrorist : Player
    {

        public Terrorist()
        {
            Role = "Terrorist";
            Weapon = "AK-47";
        }

        public override void AssignMission(string mission)
        {
            Console.WriteLine($"{Role} assigned misson {mission}");
        }

        public override void Display(int x, int y)
        {
            Console.WriteLine($"{Role} position ({x},{y}) with {Weapon}");
        }
    }
}
