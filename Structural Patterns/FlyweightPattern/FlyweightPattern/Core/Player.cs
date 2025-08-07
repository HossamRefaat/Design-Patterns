using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweightPattern.Core
{
    internal abstract class Player
    {
        public string Role { get; protected set; }
        public string Weapon { get; protected set; }

        public abstract void AssignMission(string mission);
        public abstract void Display(int x, int y);
    }
}
