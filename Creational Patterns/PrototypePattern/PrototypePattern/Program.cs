
using PrototypePattern.Core;

namespace PrototypePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Document d1 = new Document
            {
                Title = "Sample Document",
                Content = "This is a sample document content.",
                Images = new List<string> { "image1.png", "image2.png" }
            };

            Document d2 = d1.DeepClone();
            Document d3 = d1.ShallowClone();

            // Modify the original document                                         
            d1.Images.Add("image3.png");

            Console.WriteLine($"Deep clone documet:\n{d2}");
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"shallow clone documet:\n{d3}");
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Original document:\n{d1}");


        }
    }
}
