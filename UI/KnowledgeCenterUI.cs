using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Knowledge_Center.Models;
using Knowledge_Center.Services;

namespace Knowledge_Center.UI
{
    public static class KnowledgeCenterUI
    {

        /* ======================== MAIN MENU ======================== */
        public static void ShowMainMenu(KnowledgeNodeService knService, LogEntryService leService, DomainService dnService)
        {
            var knUI = new KnowledgeNodeUI(knService, leService, dnService);
            bool exit = false;

            while (!exit) 
            {
                Console.Clear();
                Console.WriteLine("=== Knowledge Center ===");
                Console.WriteLine("1. Create a Knowledge Node");
                Console.WriteLine("2. View All Knowledge Nodes");
                Console.WriteLine("3. Log Entry to a Knowledge Node");
                Console.WriteLine("4. Update a Knowledge Node");
                Console.WriteLine("5. Delete a Knowledge Node");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect an option: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        knUI.CreateNode();
                        break;
                    case "2":
                        ViewAllNodes(knService, leService, dnService);
                        break;
                    case "3":
                        CreateLogEntry(leService, knService);
                        break;
                    case "4":
                        UpdateAKnowledgeNode(knService, dnService);
                        break;
                    case "5":
                        DeleteAKnowledgeNode(knService, leService);
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


        /* ======================== KNOWLEDGE NODES ======================== */

        

        


        /* ======================== LOG ENTRIES ======================== */

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

        /* ======================== DOMAINS ======================== */

    }
}
