using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge_Center.Services.Security
{
    public class AuthSession
    {
        // username -> token
        private static Dictionary<string, string> _activeSessions = new();

        public static string CreateSession(string username) { }

        public static bool IsValidToken(string token) { }

        public static void EndSession(string username) { }
    }
}
