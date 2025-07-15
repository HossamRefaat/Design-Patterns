using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite_Pattern.Core
{
    public class Housing : IStructure
    {
        private readonly List<IStructure> structures;
        private readonly string address;

        public Housing(string address)
        {
            this.structures = new List<IStructure>();
            this.address = address;
        }

        public int AddStructure(IStructure structure)
        {
            this.structures.Add(structure);
            return this.structures.Count - 1; // Return the index of the added structure
        }

        public IStructure GetStructure(int index)
        {
            if (index < 0 || index >= this.structures.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }
            return this.structures[index];
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
            return this.address;
        }

        public void Location()
        {
            Console.WriteLine($"You are currently in the {this.GetName()}. it has ");
            foreach (var structure in this.structures)
            {
                structure.GetName();
            }
        }
    }
}
