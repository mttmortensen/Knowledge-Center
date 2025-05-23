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
        private readonly DomainController _domainController;
        private readonly LogEntryService _logEntryService;

        private readonly HttpListener _listener;

        public CoreAPI(KnowledgeNodeController knowledgeNodeController, LogEntryService logEntryService, DomainController domainController)
        {
            _knowledgeNodeController = knowledgeNodeController;
            _logEntryService = logEntryService;
            _domainController = domainController;

            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:8080/");
        }

        // Starts the server loop
        public void Start()
        {
            _listener.Start();
            Console.WriteLine("\nThe Knowledge Center API has launched as well! The server started on http://localhost:8080");
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
                        WriteResponse(response, HttpStatusCode.MethodNotAllowed, "Route not found");
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

            switch (true)
            {
                case true when route == "/api/knowledge-nodes":
                    _knowledgeNodeController.GetAll(response);
                    break;

                case true when route.StartsWith("/api/knowledge-nodes/") &&
                              int.TryParse(request.Url.Segments.Last(), out int nodeId):
                    _knowledgeNodeController.GetById(response, nodeId);
                    break;

                case true when route == "/api/domains":
                    _domainController.GetAll(response);
                    break;

                case true when route.StartsWith("/api/domains/") &&
                              int.TryParse(request.Url.Segments.Last(), out int domainId):
                    _domainController.GetById(response, domainId);
                    break;

                default:
                    WriteResponse(response, HttpStatusCode.NotFound, "Route not found");
                    break;
            }
        }

        // POST
        private void HandlePostRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string route = request.Url.AbsolutePath.ToLower();

            switch (true)
            {
                case true when route == "/api/knowledge-nodes":
                    _knowledgeNodeController.Create(response, request);
                    break;

                case true when route == "/api/domains":
                    _domainController.Create(response, request);
                    break;

                default:
                    WriteResponse(response, HttpStatusCode.NotFound, "Route not found");
                    break;
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
                WriteResponse(response, HttpStatusCode.NotFound, "Route not found");
            }
        }

        // DELETE
        private void HandleDeleteRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string route = request.Url.AbsolutePath.ToLower();

            if (route.StartsWith("/api/knowledge-nodes/") && int.TryParse(request.Url.Segments.Last(), out int nodeId))
            {
                _knowledgeNodeController.Delete(response, nodeId);
            }
            else
            {
                WriteResponse(response, HttpStatusCode.NotFound, "Route not found");
            }
        }

        // Helper method to write JSON response
        private void WriteResponse(HttpListenerResponse response, HttpStatusCode statusCode, string message)
        {
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";

            var jsonResponse = JsonSerializer.Serialize(new { message });
            byte[] buffer = Encoding.UTF8.GetBytes(jsonResponse);
            response.ContentLength64 = buffer.Length;

            using (var output = response.OutputStream)
            {
                output.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
