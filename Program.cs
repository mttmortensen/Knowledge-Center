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
            var service = new KnowledgeNodeService(db);

            KnowledgeCenterUI.ShowMainMenu(service);
        }
    }
}
