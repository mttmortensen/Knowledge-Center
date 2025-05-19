using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Knowledge_Center
{
    public static class KnowledgeCenterUI
    {

        /* ======================== MAIN MENU ========================*/
        public static void ShowMainMenu(KnowledgeNodeService knService, LogEntryService leService)
        {
            bool exit = false;

            while (!exit) 
            {
                Console.Clear();
                Console.WriteLine("=== Knowledge Center ===");
                Console.WriteLine("1. Create a Knowledge Node");
                Console.WriteLine("2. View All Knowledge Nodes");
                Console.WriteLine("3. Log Entry to a Knowledge Node");
                Console.WriteLine("4. Update a Knowledge Node");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect an option: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateNode(knService);
                        break;
                    case "2":
                        ViewAllNodes(knService, leService);
                        break;
                    case "3":
                        CreateLogEntry(leService, knService);
                        break;
                    case "4":
                        UpdateAKnowledgeNode(knService);
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


        /* ======================== KNOWLEDGE NODES ========================*/

        public static void CreateNode(KnowledgeNodeService service) 
        {
            Console.WriteLine("=== Create a Knowledge Node ===");

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Domain: ");
            string domain = Console.ReadLine();

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
                Domain = domain,
                NodeType = nodeType,
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

        private static string GetNodeType()
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

        public static void ViewAllNodes(KnowledgeNodeService knService, LogEntryService leService)
        {
            Console.Clear();
            Console.WriteLine("=== View All Knowledge Nodes ===");

            List<KnowledgeNode> nodes = knService.GetAllNodes();

            if (nodes.Count == 0)
            {
                Console.WriteLine("No nodes found.");
            }
            else
            {
                int count = 1;
                foreach (var node in nodes)
                {
                    Console.WriteLine($"[{count}] Title: {node.Title}, Created On: {node.CreatedAt}");
                    count++;
                }
                
                SelectAKnowledgeNode(nodes, knService, leService);   
            }
            
        }

        private static void SelectAKnowledgeNode(List<KnowledgeNode> nodes, KnowledgeNodeService knService, LogEntryService leService) 
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
                    ShowKnowledgeNodeDetails(selectedNode, knService, leService);
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

        private static void ShowKnowledgeNodeDetails(KnowledgeNode selectedNode, KnowledgeNodeService knService, LogEntryService leService)
        {
            Console.Clear();
            Console.WriteLine($"=== {selectedNode.Title} Details ===");
            Console.WriteLine($"Domain: {selectedNode.Domain}");
            Console.WriteLine($"Node Type: {selectedNode.NodeType}");
            Console.WriteLine($"Confidence Level: {selectedNode.ConfidenceLevel}");
            Console.WriteLine($"Status: {selectedNode.Status}");
            Console.WriteLine($"Created At: {selectedNode.CreatedAt}");
            Console.WriteLine($"Description: {selectedNode.Description}");
            Console.WriteLine($"Created: {selectedNode.CreatedAt}");
            Console.WriteLine($"Last Updated: {selectedNode.LastUpdated}");

            List<LogEntry> logEntries = leService.GetAllLogEntriesByNodeId(selectedNode.Id);

            if (logEntries != null && logEntries.Count > 0)
            {
                ShowLogEntryListAndSelect(logEntries, leService, knService);
            }
            else
            {
                Console.WriteLine("\nNo log entries found for this node.");
                Console.WriteLine("Press any key to go back to the Main Menu...");
                Console.ReadKey();
            }

            Console.WriteLine("\nPress any key to go back to the Main Menu...");
            Console.ReadKey();
        }

        public static void UpdateAKnowledgeNode(KnowledgeNodeService knService)
        {
            var nodes = knService.GetAllNodes();

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
                Console.WriteLine($"[{i + 1}] {nodes[i].Title} ({nodes[i].NodeType})");
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

            Console.Clear();
            Console.WriteLine($"=== Editing: {node.Title} ===");
            Console.WriteLine("Leave any field blank to keep the current value.");

            Console.Write($"Title ({node.Title}): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title)) node.Title = title;

            Console.Write($"Domain ({node.Domain}): ");
            string domain = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(domain)) node.Domain = domain;

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

            bool updated = knService.UpdateNode(node);

            Console.WriteLine(updated
                ? "\n✅ Knowledge Node updated successfully!"
                : "\n❌ Update failed.");

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }



        /* ======================== LOG ENTRIES ========================*/

        public static void CreateLogEntry(LogEntryService leService, KnowledgeNodeService knService)
        {
            var nodes = knService.GetAllNodes();

            if (nodes.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No Knowledge Nodes exist. Create one before adding logs.");
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Select a Knowledge Node to Add a Log ===");

            for (int i = 0; i < nodes.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {nodes[i].Title} ({nodes[i].NodeType})");
            }

            Console.Write("\nChoose a node number: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int index) || index < 1 || index > nodes.Count)
            {
                Console.WriteLine("Invalid selection. Press any key to return...");
                Console.ReadKey();
                return;
            }

            var selectedNode = nodes[index - 1];

            Console.Clear();
            Console.WriteLine($"=== Add Log to {selectedNode.Title} ===");

            Console.Write("Content: ");
            string content = Console.ReadLine();

            Console.Write("Tag: ");
            string tag = Console.ReadLine();

            Console.Write("Contributes to Progress? (true/false): ");
            bool contributes = Convert.ToBoolean(Console.ReadLine());

            var newLog = new LogEntry
            {
                NodeId = selectedNode.Id,
                EntryDate = DateTime.Now,
                Content = content,
                Tag = tag,
                ContributesToProgress = contributes
            };

            bool success = leService.CreateLogEntry(newLog);

            Console.WriteLine(success
                ? $"\n✅ Log added to '{selectedNode.Title}'"
                : "\n❌ Failed to create log entry.");

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }


        private static void ShowLogEntryListAndSelect(List<LogEntry> logEntries, LogEntryService leService, KnowledgeNodeService knService) 
        {
            Console.WriteLine($"\n=== Log Entries ===");

            for (int i = 0; i < logEntries.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {logEntries[i].EntryDate.ToShortTimeString()}");
            }

            Console.WriteLine("\nSelect a log entry number to view");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice)) 
            {
                if (choice == 0)
                {
                    return;
                }

                if (choice >= 1 && choice <= logEntries.Count)
                {
                    var selectedLog = logEntries[choice - 1];
                    ShowLogEntryDetails(selectedLog, knService);
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

        private static void ShowLogEntryDetails(LogEntry logEntry, KnowledgeNodeService knService)
        {
            var knowledgeNode = knService.GetNodeById(logEntry.NodeId);


            Console.Clear();
            Console.WriteLine($"=== Log Entry Details ===");
            Console.WriteLine($"KnowledgeNode Title: {knowledgeNode.Title}");
            Console.WriteLine($"KnowledgeNode ID: {logEntry.NodeId}");
            Console.WriteLine($"Entry Date: {logEntry.EntryDate}");
            Console.WriteLine($"Tag: {logEntry.Tag}");
            Console.WriteLine($"Contributes to Progress: {logEntry.ContributesToProgress}");
            Console.WriteLine($"\nContent: \n{logEntry.Content}");

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

    }
}
