using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Core.Commands
{
    internal class Macro
    {
        public Macro(int id, IEnumerable<ICommand> commands)
        {
            Id = id;
            Commands = commands;
        }

        public int Id { get; }
        public IEnumerable<ICommand> Commands { get; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
