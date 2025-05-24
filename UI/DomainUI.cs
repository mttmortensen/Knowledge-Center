using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Services;
using Knowledge_Center.Models;
using System.Xml.Linq;

namespace Knowledge_Center.UI
{
    public class DomainUI
    {
        private readonly KnowledgeNodeService _knService;
        private readonly LogEntryService _lgService;
        private readonly DomainService _dnService;

        // Manul Injection for Reference Later to KnowledgeNodeUI
        private KnowledgeNodeUI _knUI;
        public void InjectKnowledgeNodeUI(KnowledgeNodeUI knUI)
        {
            _knUI = knUI;
        }

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
                Console.WriteLine("3. Update a Domain");
                Console.WriteLine("4. Delete a Domain");

                Console.WriteLine("\n0. Exit");
                Console.Write("\nSelect an option: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
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
                    Console.WriteLine($"\n[{count}] Title: {domain.DomainName}, " +
                        $"\nStatus: {domain.DomainStatus}" +
                        $"\nCreated On: {domain.CreatedAt}");
                    count++;
                }

                SelectADomain(domains);
            }
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

            List<KnowledgeNode> allNodes = _knService.GetAllNodes();
            List<KnowledgeNode> domainNodes = new List<KnowledgeNode>();

            Console.WriteLine("\n=== Associated Knowledge Nodes ===");
            foreach (var node in allNodes)
            {
                if (node.DomainId == seletedDomain.DomainId)
                {
                    domainNodes.Add(node);
                    Console.WriteLine($"[{domainNodes.Count}] {node.Title} (Status: {node.Status})");
                }
            }


            if (domainNodes.Count == 0)
            {
                Console.WriteLine("No Knowledge Nodes associated with this domain.");
            }
            else
            {
                _knUI.SelectAKnowledgeNode(domainNodes);
            }

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

            Console.WriteLine($"=== Editing: {domain.DomainName} ===");
            Console.WriteLine("Leave any field blank to keep the current value.");

            Console.Write($"Name ({domain.DomainName}): ");
            string newDomainName = Console.ReadLine();

            Console.Write($"Description: ({domain.DomainDescription}): ");
            string newDomainDescription = Console.ReadLine();

            Console.Write($"Status ({domain.DomainStatus}): ");
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

            Console.WriteLine("\n=== Available Domains ===:");
            for (int i = 0; i < domainList.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {domainList[i].DomainName} ({domainList[i].DomainStatus})");
            }

            Console.Write("\nSelect a domain to delete (or 0 to cancel): ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int choice) || choice < 0 || choice > domainList.Count)
            {
                Console.WriteLine("Invalid selection. Press any key to return...");
                Console.ReadKey();
                return;
            }

            if (choice == 0) return;

            Domain domainToDelete = domainList[choice - 1];

            Console.Write($"\nAre you sure you want to delete '{domainToDelete.DomainName}'? (yes/no): ");
            string confirm = Console.ReadLine()?.Trim().ToLower();

            if (confirm != "yes")
            {
                Console.WriteLine("Deletion cancelled.");
                Console.ReadKey();
                return;
            }

            bool success = _dnService.DeleteDomain(domainToDelete.DomainId);


            Console.WriteLine(success
                ? "\n✅ Domain deleted successfully!"
                : "\n❌ Failed to delete domain.");

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}
