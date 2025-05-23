﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
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

        // === POST /api/knowledge-nodes ===
        public void Create(HttpListenerResponse response, HttpListenerRequest request)
        {

            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            string body = reader.ReadToEnd();

            KnowledgeNode node;

            try
            {
                node = JsonSerializer.Deserialize<KnowledgeNode>(body);
            }
            catch 
            {
                WriteJson(response, HttpStatusCode.BadRequest, new { message = "Invalid JSON format." });
                return;
            }

            bool success = _knowledgeNodeService.CreateNode(node);
            if (!success) 
            {
                WriteJson(response, HttpStatusCode.InternalServerError, new { message = "Failed to create node." });
                return;
            }
            
            WriteJson(response, HttpStatusCode.Created, node);
        }

        // === PUT /api/knowledge-nodes/{id} ===
        public void Update(HttpListenerResponse response, HttpListenerRequest request, int id) 
        {
            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            string body = reader.ReadToEnd();

            KnowledgeNode node;

            try
            {
                node = JsonSerializer.Deserialize<KnowledgeNode>(body);
                node.Id = id; // Set the ID for the update
            }
            catch
            {
                WriteJson(response, HttpStatusCode.BadRequest, new { message = "Invalid JSON format." });
                return;
            }

            bool success = _knowledgeNodeService.UpdateNode(node);
            if (!success)
            {
                WriteJson(response, HttpStatusCode.InternalServerError, new { message = "Node not found or updated failed." });
                return;
            }

            WriteJson(response, HttpStatusCode.OK, node);
        }

        // === DELETE /api/knowledge-nodes/{id} ===
        public void Delete(HttpListenerResponse response, int id)
        {
            bool success = _knowledgeNodeService.DeleteNode(id);
            if (!success)
            {
                WriteJson(response, HttpStatusCode.InternalServerError, new { message = "Node not found or delete failed." });
                return;
            }
            WriteJson(response, HttpStatusCode.OK, new { message = "Node deleted"}); 
        }


        // === HELPER ===
        private void WriteJson(HttpListenerResponse response, HttpStatusCode statusCode, object obj)
        {
            string json = JsonSerializer.Serialize(obj);
            byte[] buffer = Encoding.UTF8.GetBytes(json);

            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";
            response.ContentLength64 = buffer.Length;

            using Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
        }
    }
}
