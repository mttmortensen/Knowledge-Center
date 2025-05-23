using Knowledge_Center.Services;
using Knowledge_Center.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge_Center.UI
{
    public class LogEntryUI
    {
        private readonly KnowledgeNodeService _knService;
        private readonly LogEntryService _lgService;
        private readonly DomainService _dnService;

        public LogEntryUI(KnowledgeNodeService knService, LogEntryService lgService, DomainService dnService)
        {
            _knService = knService;
            _lgService = lgService;
            _dnService = dnService;
        }
        // ========================== MAIN MENU ==========================

        public void ShowLogEntryMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== LOG ENTRY MENU ===");
                Console.WriteLine("\n===================");
                Console.WriteLine(" SELECT AN OPTION");
                Console.WriteLine("===================");
                Console.WriteLine("\n1. Create Log Entry");
                Console.WriteLine("2. View All Log Entries");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        CreateLogEntry();
                        break;
                    case "2":
                        ViewAllLogEntries();
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
        public void CreateLogEntry()
        {
            var nodes = _knService.GetAllNodes();

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

            bool success = _lgService.CreateLogEntry(newLog);

            Console.WriteLine(success
                ? $"\n✅ Log added to '{selectedNode.Title}'"
                : "\n❌ Failed to create log entry.");

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }


        // READ
        public void ViewAllLogEntries() 
        {
            Console.Clear();
            Console.WriteLine("=== All Log Entries ===");
            List<LogEntry> logEntries = _lgService.GetAllLogEntries();
            if (logEntries.Count == 0)
            {
                Console.WriteLine("No log entries found.");
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
                return;
            }
            foreach (var log in logEntries)
            {
                Console.WriteLine($"[{log.LogId}] {log.EntryDate.ToShortTimeString()} - {log.Content}");
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();

        }

        public void ShowLogEntryListAndSelect(List<LogEntry> logEntries)
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
                    ShowLogEntryDetails(selectedLog);
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

        private void ShowLogEntryDetails(LogEntry logEntry)
        {
            var knowledgeNode = _knService.GetNodeById(logEntry.NodeId);


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
