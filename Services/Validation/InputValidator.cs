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

        public static void ValidateTitle(string title) { }
        public static void ValidateDescription(string description) { }
        public static void ValidateNodeType(string nodeType) { }
        public static void ValidateStatus(string status) { }
    }
}
