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

        // Manul Injection for Reference Later to DomainUI
        private DomainUI _dnUI;
        public void InjectDomainUI(DomainUI dnUI)
        {
            _dnUI = dnUI;
        }


        public KnowledgeNodeUI(KnowledgeNodeService knService, LogEntryService lgService, DomainService dnService) 
        {
            _knService = knService;
            _lgService = lgService;
            _dnService = dnService;
        }

        // ========================== MAIN MENU ==========================
        public void ShowKnowledgeNodeMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== KNOWLEDGE NODE MENU ===");
                Console.WriteLine("\n===================");
                Console.WriteLine(" SELECT AN OPTION");
                Console.WriteLine("===================");
                Console.WriteLine("1. Create a Knowledge Node");
                Console.WriteLine("2. View All Knowledge Nodes");
                Console.WriteLine("3. Update a Knowledge Node");
                Console.WriteLine("4. Delete a Knowledge Node");
                
                Console.WriteLine("\n0. Exit");
                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        CreateNode();
                        break;
                    case "2":
                        ViewAllNodes();
                        break;
                    case "3":
                        UpdateAKnowledgeNode();
                        break;
                    case "4":
                        DeleteAKnowledgeNode();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ========================== CRUD ========================== 

        // CREATE 
        public void CreateNode() 
        {
            Console.WriteLine("=== Create a Knowledge Node ===");

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
            Console.WriteLine("[C] Create a new domain");
            Console.Write("Enter the number of the domain or C to create a new one: ");
            string domainInput = Console.ReadLine();

            if(domainInput.Trim().ToLower() == "c")
            {
                _dnUI.CreateDomain();
                CreateNode(); // Re-invoke the CreateNode method to allow the user to create a node after creating a domain
                return;
            }

            if (!int.TryParse(domainInput, out int domainChoice) || domainChoice < 1 || domainChoice > allDomains.Count)
            {
                Console.WriteLine("Invalid selection. Press any key to return...");
                Console.ReadKey();
                return;
            }

            int selectedDomainId = allDomains[domainChoice - 1].DomainId;


            // === CONTINUE NORMAL FLOW ===
            Console.Write("Title: ");
            string title = Console.ReadLine();

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

        // READ
        public void ViewAllNodes()
        {

            List<KnowledgeNode> nodes = _knService.GetAllNodes();

            if (nodes.Count == 0)
            {
                Console.WriteLine("No Knowledge Nodes available.");
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("=== View All Knowledge Nodes ===");

                int count = 1;
                Domain domain;


                foreach (var node in nodes)
                {
                    domain = _dnService.GetDomainById(node.DomainId);
                    Console.WriteLine($"\n[{count}] Title: {node.Title}, " +
                        $"\nDomain: {domain.DomainName} " +
                        $"\nCreated On: {node.CreatedAt}");
                    count++;
                }

                SelectAKnowledgeNode(nodes);
            }

        }

        public void SelectAKnowledgeNode(List<KnowledgeNode> nodes)
        {
            Console.WriteLine("\nPlease a select a Knowledge Node to view it's details (0 to return): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int selection))
            {
                if (selection == 0)
                {
                    return;
                }

                if (selection >= 1 && selection <= nodes.Count)
                {
                    var selectedNode = nodes[selection - 1];
                    ShowKnowledgeNodeDetails(selectedNode);
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Please try again.");
                Console.ReadKey();
            }
        }

        private void ShowKnowledgeNodeDetails(KnowledgeNode selectedNode)
        {
            Console.Clear();
            Console.WriteLine($"=== {selectedNode.Title} Details ===");

            // ==== DOMAIN NAMING ====
            var domain = _dnService.GetDomainById(selectedNode.DomainId);
            string domainName = domain != null ? domain.DomainName : "Unknown Domain";

            Console.WriteLine($"Domain: {domainName}");

            Console.WriteLine($"Node Type: {selectedNode.NodeType}");
            Console.WriteLine($"Confidence Level: {selectedNode.ConfidenceLevel}");
            Console.WriteLine($"Status: {selectedNode.Status}");
            Console.WriteLine($"Created At: {selectedNode.CreatedAt}");
            Console.WriteLine($"Description: {selectedNode.Description}");
            Console.WriteLine($"Created: {selectedNode.CreatedAt}");
            Console.WriteLine($"Last Updated: {selectedNode.LastUpdated}");

            List<LogEntry> logEntries = _lgService.GetAllLogEntriesByNodeId(selectedNode.Id);
            LogEntryUI logEntryUI = new LogEntryUI(_knService, _lgService, _dnService);

            if (logEntries != null && logEntries.Count > 0)
            {
                logEntryUI.ShowLogEntryListAndSelect(logEntries);
            }
            else
            {
                Console.WriteLine("\nNo log entries found for this node.");
                Console.WriteLine("Press any key to go back to the Main Menu...");
                Console.ReadKey();
            }

            
        }

        // UPDATE
        public void UpdateAKnowledgeNode()
        {
            var nodes = _knService.GetAllNodes();

            if (nodes.Count == 0)
            {
                Console.WriteLine("No Knowledge Nodes available to update.");
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Update a Knowledge Node ===");

            for (int i = 0; i < nodes.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] Name: {nodes[i].Title} Type: ({nodes[i].NodeType})");
            }

            Console.Write("\nSelect a node to update (or 0 to cancel): ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int choice) || choice < 0 || choice > nodes.Count)
            {
                Console.WriteLine("Invalid selection. Press any key to return...");
                Console.ReadKey();
                return;
            }

            if (choice == 0) return;

            var node = nodes[choice - 1];

            Console.WriteLine($"=== Editing: {node.Title} ===");
            Console.WriteLine("Leave any field blank to keep the current value.");

            Console.Write($"Title ({node.Title}): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title)) node.Title = title;

            // === DOMAIN SELECTION FOR UPDATES ===
            var currentDomain = _dnService.GetDomainById(node.DomainId);
            string currentDomainName = currentDomain?.DomainName ?? "Unknown";

            Console.WriteLine($"\nCurrent Domain: {currentDomainName}");
            Console.Write("Change domain? (yes/no): ");
            string domainChangeInput = Console.ReadLine().Trim().ToLower();

            if (domainChangeInput == "yes")
            {
                var allDomains = _dnService.GetAllDomains();

                Console.WriteLine("\nAvailable Domains:");
                for (int i = 0; i < allDomains.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {allDomains[i].DomainName}");
                }

                Console.Write("Select a new domain by number: ");
                string domainInput = Console.ReadLine();

                if (int.TryParse(domainInput, out int domainChoice) &&
                    domainChoice >= 1 && domainChoice <= allDomains.Count)
                {
                    node.DomainId = allDomains[domainChoice - 1].DomainId;
                }
                else
                {
                    Console.WriteLine("Invalid domain selection. Keeping existing domain.");
                }
            }

            // === CONTINUE NORMAL FLOW ===
            Console.Write($"Description ({node.Description}): ");
            string desc = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(desc)) node.Description = desc;

            Console.Write($"Confidence Level ({node.ConfidenceLevel}): ");
            string confInput = Console.ReadLine();
            if (int.TryParse(confInput, out int confVal)) node.ConfidenceLevel = confVal;

            Console.Write($"Status ({node.Status}): ");
            string status = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(status)) node.Status = status;

            Console.Write($"Node Type ({node.NodeType}): ");
            string type = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(type)) node.NodeType = type;

            bool updated = _knService.UpdateNode(node);

            Console.WriteLine(updated
                ? "\n✅ Knowledge Node updated successfully!"
                : "\n❌ Update failed.");

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }

        // DELETE
        public void DeleteAKnowledgeNode()
        {
            var nodes = _knService.GetAllNodes();

            if (nodes.Count == 0)
            {
                Console.WriteLine("No Knowledge Nodes available to delete.");
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Delete a Knowledge Node ===");

            for (int i = 0; i < nodes.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {nodes[i].Title} ({nodes[i].NodeType})");
            }

            Console.Write("\nSelect a node to delete (or 0 to cancel): ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int choice) || choice < 0 || choice > nodes.Count)
            {
                Console.WriteLine("Invalid selection. Press any key to return...");
                Console.ReadKey();
                return;
            }

            if (choice == 0) return;

            var selectedNode = nodes[choice - 1];

            Console.Write($"\nAre you sure you want to permanently delete '{selectedNode.Title}' and all its logs? (yes/no): ");
            string confirm = Console.ReadLine()?.Trim().ToLower();

            if (confirm != "yes")
            {
                Console.WriteLine("\nDeletion cancelled. Press any key to return...");
                Console.ReadKey();
                return;
            }

            bool logsDeleted = _lgService.DeleteAllLogEntriesByNodeId(selectedNode.Id);
            bool nodeDeleted = _knService.DeleteNode(selectedNode.Id);

            Console.WriteLine((logsDeleted && nodeDeleted)
                ? $"\n✅ '{selectedNode.Title}' and all its logs were deleted successfully."
                : "\n❌ Deletion failed. Some data may still exist.");

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
