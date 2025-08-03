using CommandPattern.Core;
using CommandPattern.Core.Commands;

namespace CommandPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var laptop = new Product(1, "Laptop", 20000, 10);
            var keyboard = new Product(2, "Keyboard", 300, 50);
            var mouse = new Product(1, "Mouse", 150, 70);

            while (true)
            {
                var order = new Order();
                var invoker = new CommandInvoker();

                while (true)
                {
                    Console.WriteLine("Select one of the below commands: ");
                    Console.WriteLine("\t1. Add Laptop");
                    Console.WriteLine("\t2. Add Keyboard");
                    Console.WriteLine("\t3. Add Mouse");
                    Console.WriteLine("\t4. Save Macro");
                    Console.WriteLine("\t5. Replay Macro");
                    Console.WriteLine("\t6. Undo");
                    Console.WriteLine("\t7. Redo");
                    Console.WriteLine("\t0. Process");

                    var commandId = int.Parse(Console.ReadLine());

                    Product? selectedProduct = null;
                    if (commandId == 1)
                    {
                        invoker.ExecuteCommand(new AddProductCommand(order, laptop, 1));
                        invoker.ExecuteCommand(new AddStockCommand(laptop, -1));
                    }
                    else if (commandId == 2)
                    {
                        invoker.ExecuteCommand(new AddProductCommand(order, keyboard, 1));
                        invoker.ExecuteCommand(new AddStockCommand(keyboard, -1));
                    }
                    else if (commandId == 3)
                    {
                        invoker.ExecuteCommand(new AddProductCommand(order, mouse, 1));
                        invoker.ExecuteCommand(new AddStockCommand(mouse, -1));
                    }
                    else if (commandId == 4)
                    {
                        MacroStorage.Instance.CreateMacro(invoker.GetCommands());
                        invoker.ClearCommand();
                    }
                    else if (commandId == 5)
                    {
                        ReplayMacro();
                    }
                    else if(commandId == 6)
                    {
                        invoker.Undo();
                        invoker.Undo();
                        var totalQuantity = order.Lines.Sum(x => x.Quantity);
                        var totalPrice = order.Lines.Sum(x => x.UnitPrice * x.Quantity);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Order #{order.Id} updated, total quantity: {totalQuantity}, total price: {totalPrice}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (commandId == 7)
                    {
                        invoker.Redo();
                        invoker.Redo();
                        var totalQuantity = order.Lines.Sum(x => x.Quantity);
                        var totalPrice = order.Lines.Sum(x => x.UnitPrice * x.Quantity);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"Order #{order.Id} updated, total quantity: {totalQuantity}, total price: {totalPrice}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (commandId == 0)
                    {
                        var totalQuantity = order.Lines.Sum(x => x.Quantity);
                        var totalPrice = order.Lines.Sum(x => x.UnitPrice * x.Quantity);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Order #{order.Id} processed, total quantity: {totalQuantity}, total price: {totalPrice}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
                Console.WriteLine("-------------------------------------------------");
            }

            

        }

        private static void ReplayMacro()
        {
            Console.WriteLine("Enter Macro Id");
            foreach(var macro in MacroStorage.Instance.GetMacros())
            {
                Console.WriteLine($"\t{macro.Id}. (Command Count: {macro.Commands.Count()}, Created At: {macro.CreatedAt:yyyy-MM-dd HH:mm:ss})");
            }

            var macroId = int.Parse(Console.ReadLine());
            var selectedMacro = MacroStorage.Instance.GetMacro(macroId);
            var order = new Order();
            var invoker = new CommandInvoker();
            foreach(var command in selectedMacro.Commands)
            {
                if (command is AddProductCommand addProductCommand)
                    addProductCommand.Order = order;
                invoker.AddCommand(command);
            }
            invoker.ExecuteCommands();
        }
    }
}
