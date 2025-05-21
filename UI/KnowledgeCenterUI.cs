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
            var leUI = new LogEntryUI(knService, leService, dnService);


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
                        knUI.ViewAllNodes();
                        break;
                    case "3":
                        leUI.CreateLogEntry();
                        break;
                    case "4":
                        knUI.UpdateAKnowledgeNode();
                        break;
                    case "5":
                        knUI.DeleteAKnowledgeNode();
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

        /* ======================== DOMAINS ======================== */

    }
}
