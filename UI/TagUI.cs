using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Services;

namespace Knowledge_Center.UI
{
    public class TagUI
    {
        private readonly TagService _tagService;

        public TagUI(TagService tagService)
        {
            _tagService = tagService;
        }

        // ========================== MAIN MENU ==========================
        public void ShowTagMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== TAG MANAGEMENT ===");
                Console.WriteLine("1. Create New Tag");
                Console.WriteLine("2. View All Tags");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("\nSelect an option: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        CreateTag();
                        break;
                    case "2":
                        ViewAllTags();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
        // ========================== CRUD ==========================
    }
}
