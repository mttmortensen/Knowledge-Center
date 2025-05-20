using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Models;
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
            List<KnowledgeNode> knowledgeNodes = _knowledgeNodeService.GetAllNodes();

            // Convert to JSON
            WriteJson(response, HttpStatusCode.OK, knowledgeNodes);
        }

        // === GET /api/knowledge-nodes/{id} ===
        public void GetById(HttpListenerResponse response, int id)
        {
            var node = _knowledgeNodeService.GetNodeById(id);

            if (node == null)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            // Convert to JSON
            WriteJson(response, HttpStatusCode.OK, node);
        }


        // === HELPER ===
        private void WriteJson(HttpListenerResponse response, HttpStatusCode statusCode, object obj)
        {
            throw new NotImplementedException();
        }
    }
}
