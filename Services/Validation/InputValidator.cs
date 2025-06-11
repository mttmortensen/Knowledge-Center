using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge_Center.Services.Validation
{
    public class InputValidator
    {
        private static readonly HashSet<string> ValidNodeTypes = new(){ "Concept", "Project" };
        private static readonly HashSet<string> ValidStatuses  = new(){ "Exploring", "Learning", "Mastered" };

        public static void ValidateTitle(string title) 
        {
            if(string.IsNullOrWhiteSpace(title)) 
            {
                throw new ArgumentException("Title is Required.");
            }

            if(title.Length > 100) 
            {
                throw new ArgumentException("Title cannot exceed 100 characters.");
            }
        }
        public static void ValidateDescription(string description) 
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.");

            if (description.Length > 500)
                throw new ArgumentException("Description cannot exceed 500 characters.");
        }
        public static void ValidateNodeType(string nodeType) 
        {
            if (!ValidNodeTypes.Contains(nodeType))
            {
                throw new ArgumentException($"Invalid NodeType: {nodeType}");
            }
        }
        public static void ValidateStatus(string status) { }
    }
}
