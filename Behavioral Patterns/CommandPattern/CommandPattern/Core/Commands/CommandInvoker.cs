using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Core.Commands
{
    //optional commponent of the Command Pattern

    internal class CommandInvoker
    {
        private List<ICommand> commands = new();
        private Stack<ICommand> executedCommands = new();   
        private Stack<ICommand> undoneCommands = new();   

        public void AddCommand(ICommand command)
        {
            commands.Add(command);
        }

        public void ExecuteCommands()
        {
            foreach (var command in commands)
            {
                ExecuteCommand(command);
            }
            ClearCommand(); 
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            executedCommands.Push(command);
        }

        public void Undo()
        {
            var command = executedCommands.Pop();
            command.Undo();
            undoneCommands.Push(command);
        }

        public void Redo()
        {
            if (undoneCommands.Count > 0)
            {
                var command = undoneCommands.Pop();
                ExecuteCommand(command);
            }
        }

        public void ClearCommand()
        {
            commands.Clear();
        }

        public IEnumerable<ICommand> GetCommands() => commands.AsReadOnly();

    }
}
