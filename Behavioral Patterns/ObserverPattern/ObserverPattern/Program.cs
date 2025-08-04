using ObserverPattern.Core;

namespace ObserverPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Simple Observer Pattern Demo ===");
            Console.WriteLine("This shows how blogs notify subscribers when new posts are published\n");

            // Step 1: Create a blog (this is the SUBJECT)
            var techBlog = new Blog("TechBlog");

            // Step 2: Create some users (these are the OBSERVERS)
            var alice = new User("Alice");
            var bob = new User("Bob");
            var charlie = new User("Charlie");

            Console.WriteLine("STEP 1: Users subscribe to the blog");
            Console.WriteLine("====================================");
            
            // Step 3: Users subscribe to the blog
            alice.SubscribeTo(techBlog);
            bob.SubscribeTo(techBlog);
            charlie.SubscribeTo(techBlog);

            // Show current subscribers
            techBlog.ShowSubscribers();

            Console.WriteLine("\nSTEP 2: Blog publishes a new post");
            Console.WriteLine("==================================");
            
            // Step 4: Blog publishes a post - all subscribers get notified automatically!
            techBlog.PublishPost("Introduction to Observer Pattern", "John Developer");

            Console.WriteLine("\nSTEP 3: One user unsubscribes");
            Console.WriteLine("==============================");
            
            // Step 5: Bob unsubscribes
            bob.UnsubscribeFrom(techBlog);

            // Show updated subscribers
            techBlog.ShowSubscribers();

            Console.WriteLine("\nSTEP 4: Blog publishes another post");
            Console.WriteLine("====================================");
            
            // Step 6: Publish another post - only remaining subscribers get notified
            techBlog.PublishPost("Advanced Design Patterns", "Jane Architect");

            Console.WriteLine("\nSTEP 5: Show what happened");
            Console.WriteLine("===========================");
            Console.WriteLine("Notice how:");
            Console.WriteLine("1. When the blog published the first post, ALL 3 users were notified");
            Console.WriteLine("2. When Bob unsubscribed, he was removed from the subscriber list");
            Console.WriteLine("3. When the blog published the second post, only Alice and Charlie were notified");
            Console.WriteLine("4. The blog doesn't need to know WHO the subscribers are - it just notifies everyone on its list");
            
            Console.WriteLine("\nThis is the Observer Pattern in action!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
