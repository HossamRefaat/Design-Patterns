using BridgePattern.Core;

namespace BridgePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var windowsButton = new Button(new Windows());
            windowsButton.Click();
        }
    }
}
