using System;
using System.Net;
using System.IO;
using System.Text;
using System.Text.Json;
using Knowledge_Center.Services;
using Knowledge_Center.Models;
using System.Security.Cryptography;
using Knowledge_Center.API.Controllers;

namespace Knowledge_Center.API
{
    public class CoreAPI
    {
        private readonly KnowledgeNodeController _knowledgeNodeController;
        private readonly LogEntryService _logEntryService;
        private readonly DomainService _domainService;

        private readonly HttpListener _listener;

        public CoreAPI(KnowledgeNodeController knowledgeNodeController, LogEntryService logEntryService, DomainService domainService)
        {
            _knowledgeNodeController = knowledgeNodeController;
            _logEntryService = logEntryService;
            _domainService = domainService;

            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:8080/");
        }

        // Starts the server loop
        public void Start()
        {
            _listener.Start();
            Console.WriteLine("🚀 API server started on http://localhost:5000");
            while (true)
            {
                var context = _listener.GetContext();
                HandleRequest(context);
            }
        }

        // Main handler for incoming requests
        private void HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            string method = request.HttpMethod;

            try
            {
                // ==== ROUTES ====
                // Handle different HTTP methods
                switch (method)
                {
                    case "GET":
                        HandleGetRequest(request, response);
                        break;
                    case "POST":
                        HandlePostRequest(request, response);
                        break;
                    case "PUT":
                        HandlePutRequest(request, response);
                        break;
                    case "DELETE":
                        HandleDeleteRequest(request, response);
                        break;
                    default:
                        WriteResponse(response, HttpStatusCode.MethodNotAllowed, "Method not allowed");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                WriteResponse(response, HttpStatusCode.InternalServerError, "Internal server error");
            }
        }

        // GET 
        private void HandleGetRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string route = request.Url.AbsolutePath.ToLower();

            if (route == "/api/knowledge-nodes")
            {
                _knowledgeNodeController.GetAll(response);
            }
            else if (route.StartsWith("/api/knowledge-nodes/") && int.TryParse(request.Url.Segments.Last(), out int nodeId))
            {
               _knowledgeNodeController.GetById(response, nodeId);
            }
            else
            {
                WriteResponse(response, HttpStatusCode.NotFound, "Not found");
            }
        }

        // POST
        private void HandlePostRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string route = request.Url.AbsolutePath.ToLower();

            if (route == "/api/knowledge-nodes")
            {
                _knowledgeNodeController.Create(response, request);
            }
            else 
            {
                WriteResponse(response, HttpStatusCode.NotFound, "Method not allowed");
            }
        }

        // PUT
        private void HandlePutRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string route = request.Url.AbsolutePath.ToLower();

            if (route.StartsWith("/api/knowledge-nodes/") && int.TryParse(request.Url.Segments.Last(), out int nodeId))
            {
                _knowledgeNodeController.Update(response, request, nodeId);
            }
            else 
            {
                WriteResponse(response, HttpStatusCode.NotFound, "Method not allowed");
            }
        }

        // DELETE
        private void HandleDeleteRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            throw new NotImplementedException();
        }

        // Helper method to write JSON response
        private void WriteResponse(HttpListenerResponse response, HttpStatusCode methodNotAllowed, string v)
        {
            throw new NotImplementedException();
        }
    }
}
