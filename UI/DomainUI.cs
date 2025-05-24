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
                Console.WriteLine("\n===================");
                Console.WriteLine(" SELECT AN OPTION");
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
                        Console.Clear(); // Clearing the console here due to CreateDomain() also being used in creating a KN 
                        CreateDomain();
                        break;
                    case "2":
                        ViewAllDomains();
                        break;
                    case "3":
                        UpdateDomain();
                        break;
                    case "4":
                        DeleteDomain();
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

        // READ
        public void ViewAllDomains()
        {
            Console.Clear();
            Console.WriteLine("=== All Domains ===");
            List<Domain> domains = _dnService.GetAllDomains();
            if (domains.Count == 0)
            {
                Console.WriteLine("No domains found.");
            }
            else
            {
                int count = 1;
                foreach(var domain in domains)
                {
                    Console.WriteLine($"[{count}] Name: {domain.DomainName}, Status: {domain.DomainStatus}");
                    count++;
                }

                SelectADomain(domains);
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void SelectADomain(List<Domain> domains)
        {
            Console.WriteLine("\nPlease a select a Domain to view it's details (0 to return): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int selection))
            {
                if (selection == 0)
                {
                    return;
                }

                if (selection >= 1 && selection <= domains.Count)
                {
                    var selectedDomain = domains[selection - 1];
                    ShowDomainDetails(selectedDomain);
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

        private void ShowDomainDetails(Domain seletedDomain)
        {
            Console.Clear();
            Console.WriteLine($"=== {seletedDomain.DomainName} Details ===");

            Console.WriteLine($"Domain Description: {seletedDomain.DomainDescription}");
            Console.WriteLine($"Domain Status: {seletedDomain.DomainStatus}");
            Console.WriteLine($"Created At: {seletedDomain.CreatedAt}");
            Console.WriteLine($"Last Used: {seletedDomain.LastUsed}");

            List<KnowledgeNode> nodes = _knService.GetAllNodes();
            

            Console.WriteLine("\nPress any key to go back to the Main Menu...");
            Console.ReadKey();
        }

        // UPDATE
        public void UpdateDomain() 
        {
            Console.Clear();
            Console.WriteLine("=== Update Domain ===");
            List<Domain> domains = _dnService.GetAllDomains();
            if (domains.Count == 0)
            {
                Console.WriteLine("No domains found.");
            }
            else
            {
                int count = 1;
                foreach (var singleDomain in domains)
                {
                    Console.WriteLine($"[{count}] Name: {singleDomain.DomainName}, Status: {singleDomain.DomainStatus}");
                    count++;
                }

            }

            Console.Write("\nSelect a domain to update (or 0 to cancel): ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int choice) || choice < 0 || choice > domains.Count)
            {
                Console.WriteLine("Invalid selection. Press any key to return...");
                Console.ReadKey();
                return;
            }

            if (choice == 0) return;

            Domain domain = domains[choice - 1];  // Get domain from list index


            if (domain == null)
            {
                Console.WriteLine("Domain not found. Press any key to return...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Current Name: {domain.DomainName}");
            Console.Write("New Domain Name (leave blank to keep current): ");
            string newDomainName = Console.ReadLine();

            Console.WriteLine($"Current Description: {domain.DomainDescription}");
            Console.Write("New Domain Description (leave blank to keep current): ");
            string newDomainDescription = Console.ReadLine();

            Console.WriteLine($"Current Status: {domain.DomainStatus}");
            Console.Write("New Domain Status (Active/Inactive, leave blank to keep current): ");
            string newDomainStatus = Console.ReadLine();

            if (!string.IsNullOrEmpty(newDomainName))
            {
                domain.DomainName = newDomainName;
            }

            if (!string.IsNullOrEmpty(newDomainDescription))
            {
                domain.DomainDescription = newDomainDescription;
            }

            if (!string.IsNullOrEmpty(newDomainStatus))
            {
                domain.DomainStatus = newDomainStatus;
            }

            bool success = _dnService.UpdateDomain(domain);

            Console.WriteLine(success
                ? "\n✅ Domain updated successfully!"
                : "\n❌ Failed to update domain.");

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        // DELETE
        public void DeleteDomain() 
        {
            
            Console.Clear();
            Console.WriteLine("=== Delete Domain ===");
            Console.WriteLine("\n=== Available Domains ===:");
            List<Domain> domainList = _dnService.GetAllDomains();

            foreach (Domain domain in domainList) 
            {
                Console.WriteLine($" Name: {domain.DomainName}, Status: {domain.DomainStatus}");
            }

            Console.Write("\nEnter Domain Name to delete: ");
            string domainName = Console.ReadLine();

            bool success = false;
            foreach (Domain domain in domainList) 
            {
                if (domain.DomainName.Equals(domainName, StringComparison.OrdinalIgnoreCase))
                {
                    success = _dnService.DeleteDomain(domain.DomainId);
                    break;
                }
            }

            Console.WriteLine(success
                ? "\n✅ Domain deleted successfully!"
                : "\n❌ Failed to delete domain.");

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}
