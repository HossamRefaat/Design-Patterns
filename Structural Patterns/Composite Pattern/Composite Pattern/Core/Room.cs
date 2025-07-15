using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite_Pattern.Core
{
    public class Room : IStructure
    {
        private readonly string name;

        public Room(string name)
        {
            this.name = name;
        }

        public void Enter()
        {
            Console.WriteLine($"You have entered the {this.GetName()}");
        }

        public void Exit()
        {
            Console.WriteLine($"You have left the {this.GetName()}");
        }

        public string GetName()
        {
            return this.name;
        }

        public void Location()
        {
            Console.WriteLine($"You are currently in the {this.GetName()}");
        }
    }
}
