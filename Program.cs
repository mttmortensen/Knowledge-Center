using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Knowledge_Center
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database("Server=MORTENSENS-MPC\\SQLEXPRESS;Database=KnowledgeCenterDB;Trusted_Connection=True;TrustServerCertificate=True;");
            KnowledgeNodeService knService = new KnowledgeNodeService(db);
            LogEntryService leService = new LogEntryService(db);

            KnowledgeCenterUI.ShowMainMenu(knService, leService);
        }
    }
}
