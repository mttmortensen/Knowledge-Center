﻿using System;
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
            TagService tgService = new TagService(db);

            KnowledgeNodeController knController = new KnowledgeNodeController(knService);
            DomainController dnController = new DomainController(dnService);
            LogEntryController lgController = new LogEntryController(leService);
            TagController tgController = new TagController(tgService);

            CoreAPI coreAPI = new CoreAPI(knController, lgController, dnController, tgController);

            // Start the API server in a separate thread
            Thread apiThread = new Thread(() => coreAPI.Start());
            apiThread.IsBackground = true;
            apiThread.Start();

            // Now run the UI 
            LogEntryUI logEntryUI = new LogEntryUI(knService, leService, dnService);
            TagUI tagUI = new TagUI(tgService);

            // Step 1: construct with nulls
            var domainUI = new DomainUI(knService, leService, dnService); // no knUI yet
            var knowledgeNodeUI = new KnowledgeNodeUI(knService, leService, dnService); // no domainUI yet

            // Step 2: inject the circular references
            domainUI.InjectKnowledgeNodeUI(knowledgeNodeUI);
            knowledgeNodeUI.InjectDomainUI(domainUI);

            // Now it's safe to start the app
            KnowledgeCenterUI.ShowMainMenu(knowledgeNodeUI, logEntryUI, domainUI, tagUI);
        }
    }
}
