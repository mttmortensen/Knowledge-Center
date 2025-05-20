using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Services;

namespace Knowledge_Center.API.Controllers
{
    public class KnowledgeNodeController
    {
        private readonly KnowledgeNodeService _knowledgeNodeService;

        public KnowledgeNodeController(KnowledgeNodeService knService) 
        {
            _knowledgeNodeService = knService;
        }

        // === GET /api/knowledge-nodes ===
        public void GetAll(HttpListenerResponse response) 
        {
            //Get All Knowledge Nodes
            throw new NotImplementedException();
        }
    }
}
