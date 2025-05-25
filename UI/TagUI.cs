using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Services;
using Knowledge_Center.Models;

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
                Console.WriteLine("3. Update a Tag");
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
                    case "3":
                        UpdateTag();
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

        // CREATE 
        public void CreateTag() 
        {
            Console.WriteLine(" === Create a New Tag ===");

            Console.Write("Enter Tag Name: ");
            string tagName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(tagName))
            {
                Console.WriteLine("Tag name cannot be empty.");
                return;
            }

            // Create a new tag object
            Tags newTag = new Tags
            {
                Name = tagName
            };

            // Call the service to create the tag
            bool success = _tagService.CreateTag(newTag);
            if (success)
            {
                Console.WriteLine("Tag created successfully!");
            }
            else
            {
                Console.WriteLine("Failed to create tag. Please try again.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // READ
        public void ViewAllTags()
        {
            Console.WriteLine("=== All Tags ===");
            List<Tags> tags = _tagService.GetAllTags();
            if (tags.Count == 0)
            {
                Console.WriteLine("No tags found.");
            }
            else
            {
                foreach (var tag in tags)
                {
                    Console.WriteLine($"Tag ID: {tag.TagId}, Name: {tag.Name}");
                }
            }
            Console.WriteLine("Press any key to continue to return to the Main Menu...");
            Console.ReadKey();
        }

        // UPDATE
        public void UpdateTag()
        {
            Console.Clear();
            Console.WriteLine("=== Update Tag ===");
            List<Tags> tags = _tagService.GetAllTags();

            if (tags.Count == 0)
            {
                Console.WriteLine("No tags found.");

            }
            else 
            {
                int count = 1;
                foreach (var tag in tags)
                {
                    Console.WriteLine($"{count}. Tag Name: {tag.Name}");
                    count++;
                }
            }

            Console.Write("\nEnter the number of the tag you want to update (or 0 to cancel): ");
            string input = Console.ReadLine();

            if(!int.TryParse(input, out int tagNumber) || tagNumber < 0 || tagNumber > tags.Count)
            {
                Console.WriteLine("Invalid input. Returning to the main menu.");
                Console.ReadKey();
                return;
            }

            if (tagNumber == 0) return;

            Tags tagToUpdate = tags[tagNumber - 1];

            if (tagToUpdate == null)
            {
                Console.WriteLine("Tag not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"=== Editing Tag: {tagToUpdate.Name} ===");
            Console.WriteLine("Leave any field blank to keep the current value.");

            Console.Write($"Tag Name ({tagToUpdate.Name}):");
            string newTagName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTagName))
            {
                tagToUpdate.Name = newTagName;
            }

            // Call the service to update the tag
            bool success = _tagService.UpdateTag(tagToUpdate);

            Console.WriteLine(success
                ? "\n✅ Tag updated successfully!"
                : "\n❌ Failed to update tag.");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
