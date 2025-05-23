using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading;
using Knowledge_Center.Services;
using Knowledge_Center.API;
using Knowledge_Center.API.Controllers;
using Knowledge_Center.UI;


namespace Knowledge_Center
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database("Server=MORTENSENS-MPC\\SQLEXPRESS;Database=KnowledgeCenterDB;Trusted_Connection=True;TrustServerCertificate=True;");
            KnowledgeNodeService knService = new KnowledgeNodeService(db);
            LogEntryService leService = new LogEntryService(db);
            DomainService dnService = new DomainService(db);

            KnowledgeNodeController knController = new KnowledgeNodeController(knService);
            DomainController dnController = new DomainController(dnService);
            LogEntryController lgController = new LogEntryController(leService);

            CoreAPI coreAPI = new CoreAPI(knController, lgController, dnController);

            // Start the API server in a separate thread
            Thread apiThread = new Thread(() => coreAPI.Start());
            apiThread.IsBackground = true;
            apiThread.Start();

            // Now run the UI 
            KnowledgeCenterUI.ShowMainMenu(knService, leService, dnService);
        }
    }
}
