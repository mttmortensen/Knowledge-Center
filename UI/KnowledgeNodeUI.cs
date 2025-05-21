using Knowledge_Center.Models;
using Knowledge_Center.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge_Center.UI
{
    public class KnowledgeNodeUI
    {
        private readonly KnowledgeNodeService _knService;
        private readonly LogEntryService _lgService;
        private readonly DomainService _dnService;


        public KnowledgeNodeUI(KnowledgeNodeService knService, LogEntryService lgService, DomainService dnService) 
        {
            _knService = knService;
            _lgService = lgService;
            _dnService = dnService;

        }

        public void CreateNode() 
        {
            Console.WriteLine("=== Create a Knowledge Node ===");

            Console.Write("Title: ");
            string title = Console.ReadLine();

            // === DOMAIN SELECTION ===

            var allDomains = _dnService.GetAllDomains();

            if (allDomains.Count == 0)
            {
                Console.WriteLine("No domains available. Please create a domain first.");
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nSelect a Domain:");
            for (int i = 0; i < allDomains.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {allDomains[i].DomainName}");
            }
            Console.Write("Enter the number of the domain: ");
            string domainInput = Console.ReadLine();

            if (!int.TryParse(domainInput, out int domainChoice) || domainChoice < 1 || domainChoice > allDomains.Count)
            {
                Console.WriteLine("Invalid selection. Press any key to return...");
                Console.ReadKey();
                return;
            }

            int selectedDomainId = allDomains[domainChoice - 1].DomainId;


            // === CONTINUE NORMAL FLOW ===

            string nodeType = GetNodeType();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Confidence Level (1-10): ");
            int confidence = Convert.ToInt32(Console.ReadLine());

            Console.Write("Status (Exploring, Learning, Mastered): ");
            string status = Console.ReadLine();

            var newNode = new KnowledgeNode
            {
                Title = title,
                DomainId = selectedDomainId,
                NodeType = nodeType,
                Description = description,
                ConfidenceLevel = confidence,
                Status = status
                // CreatedAt and LastUpdated get auto-set in the service
            };

            bool success = _knService.CreateNode(newNode);

            Console.WriteLine(success
                ? "\n🎉 Node created successfully!"
                : "\n❌ Failed to create node.");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

        }

        private string GetNodeType()
        {
            Console.WriteLine("Select Node Type:");
            Console.WriteLine("1. Concept");
            Console.WriteLine("2. Project");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice) 
            {
                case "1":
                    choice = "Concept";
                    break;
                case "2":
                    choice = "Project";
                    break;
            }

            return choice;
        }
    }
}
