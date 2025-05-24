using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Knowledge_Center.Services;

namespace Knowledge_Center.UI
{
    public static class KnowledgeCenterUI
    {

        /* ======================== MAIN MENU ======================== */
        public static void ShowMainMenu(KnowledgeNodeUI knUI, LogEntryUI leUI, DomainUI dnUI)
        {

            bool exit = false;

            while (!exit) 
            {
                Console.Clear();
                Console.WriteLine("=== KNOWLEDGE CENTER HOME ===");

                Console.WriteLine("\n=================");
                Console.WriteLine("   QUICK LINKS");
                Console.WriteLine("=================");
                Console.WriteLine("1. Enter a Log");
                Console.WriteLine("2. Create a New Knowledge Node");

                Console.WriteLine("\n=================");
                Console.WriteLine(" SELECT AN OPTION");
                Console.WriteLine("=================");
                Console.WriteLine("3. Knowledge Nodes");
                Console.WriteLine("4. Domains");
                Console.WriteLine("5. Log Entries");

                Console.WriteLine("\n0. Exit");
                Console.Write("\nSelect an option: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        leUI.CreateLogEntry();
                        break;
                    case "2":
                        knUI.CreateNode();
                        break;
                    case "3":
                        knUI.ShowKnowledgeNodeMenu();
                        break;
                    case "4":
                        dnUI.ShowDomainMenu();
                        break;
                    case "5":
                        leUI.ShowLogEntryMenu();
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

    }
}
