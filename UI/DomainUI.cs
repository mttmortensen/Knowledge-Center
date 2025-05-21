using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Services;
using Knowledge_Center.Models;

namespace Knowledge_Center.UI
{
    public class DomainUI
    {
        private readonly KnowledgeNodeService _knService;
        private readonly LogEntryService _lgService;
        private readonly DomainService _dnService;
        public DomainUI(KnowledgeNodeService knService, LogEntryService lgService, DomainService dnService)
        {
            _knService = knService;
            _lgService = lgService;
            _dnService = dnService;
        }

        // ========================== MAIN MENU ==========================
        public void ShowDomainMenu()
        {
            bool exit = false; 

            while(!exit) 
            {
                Console.Clear();
                Console.WriteLine("=== DOMAIN MENU ===");
                Console.WriteLine("\n=================");
                Console.WriteLine("   SELECT AN OPTION");
                Console.WriteLine("===================");
                Console.WriteLine("1. Create a New Domain");
                Console.WriteLine("2. View All Domains");
                Console.WriteLine("3. Edit Domain");
                Console.WriteLine("4. Delete Domain");

                Console.WriteLine("\n0. Exit");
                Console.Write("\nSelect an option: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        CreateDomain();
                        break;
                    case "2":
                        //ViewAllDomains();
                        break;
                    case "3":
                        // EditDomain();
                        break;
                    case "4":
                        // DeleteDomain();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Press any key to return...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ========================== CRUD ========================== 


        // CREATE 
        public void CreateDomain()
        {
            Console.Clear();
            Console.WriteLine("=== Create a New Domain ===");

            Console.Write("Domain Name: ");
            string domainName = Console.ReadLine();

            Console.Write("Domain Description: ");
            string domainDescription = Console.ReadLine();

            Console.Write("Domain Status (Active/Inactive): ");
            string domainStatus = Console.ReadLine();

            var newDomain = new Domain
            {
                DomainName = domainName,
                DomainDescription = domainDescription,
                DomainStatus = domainStatus
            };

            bool success = _dnService.CreateDomain(newDomain);
            if (success)
            {
                Console.WriteLine("Domain created successfully!");
            }
            else
            {
                Console.WriteLine("Failed to create domain.");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}
