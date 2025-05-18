using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Knowledge_Center
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database("Server=MORTENSENS-MPC\\SQLEXPRESS;Database=KnowledgeCenterDB;Trusted_Connection=True;TrustServerCertificate=True;");
            var service = new KnowledgeNodeService(db);

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

            Console.ReadLine();
        }
    }
}
