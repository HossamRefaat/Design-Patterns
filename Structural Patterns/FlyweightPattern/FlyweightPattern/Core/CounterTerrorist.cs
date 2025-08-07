using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweightPattern.Core
{
    internal class CounterTerrorist : Player
    {
        public CounterTerrorist()
        {
            Role = "Counter-Terrorist";
            Weapon = "M4A1-S";
        }

        public override void AssignMission(string mission)
        {
            Console.WriteLine($"{Role} assigned mission: {mission}");
        }

        public override void Display(int x, int y)
        {
            Console.WriteLine($"{Role} at position ({x},{y}) with {Weapon}");
        }
    }
}
