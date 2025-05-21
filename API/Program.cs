using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Services;
using Knowledge_Center.API.Controllers;

namespace Knowledge_Center.API
{
    public class Program
    {
        static void Main() 
        {
            var db = new Database("Server=MORTENSENS-MPC\\SQLEXPRESS;Database=KnowledgeCenterDB;Trusted_Connection=True;TrustServerCertificate=True;");

            KnowledgeNodeService knService = new KnowledgeNodeService(db);
            LogEntryService leService = new LogEntryService(db);
            DomainService dnService = new DomainService(db);

            KnowledgeNodeController knController = new KnowledgeNodeController(knService);
            CoreAPI coreAPI = new CoreAPI(knController, leService, dnService);

            // Start the API server
            coreAPI.Start();
        }
    }
}
