using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge_Center
{
    public static class KnowledgeCenterUI
    {

        public static void ShowMainMenu(KnowledgeNodeService service)
        {
            bool exit = false;

            while (!exit) 
            {
                Console.Clear();
                Console.WriteLine("=== Knowledge Center ===");
                Console.WriteLine("1. Create a Knowledge Node");
                Console.WriteLine("2. View All Knowledge Nodes");
                Console.WriteLine("3. Exit");
                Console.Write("\nSelect an option: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateNode(service);
                        break;
                    case "2":
                        ViewAllNodes(service);
                        break;
                    case "3":
                        Console.WriteLine("\n Log Entry feature coming soon! \nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }
            }

        }

        public static void CreateNode(KnowledgeNodeService service) 
        {
            Console.WriteLine("=== Create a Knowledge Node ===");

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Domain: ");
            string domain = Console.ReadLine();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Confidence Level (1-10): ");
            int confidence = Convert.ToInt32(Console.ReadLine());

            Console.Write("Status (Exploring, Learning, Mastered): ");
            string status = Console.ReadLine();

            var newNode = new KnowledgeNode
            {
                Title = title,
                Domain = domain,
                Description = description,
                ConfidenceLevel = confidence,
                Status = status
                // CreatedAt and LastUpdated get auto-set in the service
            };

            bool success = service.CreateNode(newNode);

            Console.WriteLine(success
                ? "\n🎉 Node created successfully!"
                : "\n❌ Failed to create node.");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

        }

        public static void ViewAllNodes(KnowledgeNodeService service)
        {
            Console.Clear();
            Console.WriteLine("=== View All Knowledge Nodes ===");

            List<KnowledgeNode> nodes = service.GetAllNodes();

            if (nodes.Count == 0)
            {
                Console.WriteLine("No nodes found.");
            }
            else
            {
                foreach (var node in nodes)
                {
                    Console.WriteLine
                    (
                        $"ID: {node.Id}, " +
                        $"Title: {node.Title}, " +
                        $"Domain: {node.Domain}, " +
                        $"Confidence Level: {node.ConfidenceLevel}, " +
                        $"Status: {node.Status}"
                    );
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
