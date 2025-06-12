using Azure.Core;
using Knowledge_Center.Models;
using Knowledge_Center.Services.Core;
using Knowledge_Center.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Knowledge_Center.API.Controllers
{
    public class DomainController
    {
        private readonly DomainService _dnService;

        public DomainController(DomainService dnService)
        {
            _dnService = dnService;
        }

        // === GET /api/domains ===
        public void GetAll(HttpListenerResponse response)
        {
            //Get All Domains
            List<Domain> domains = _dnService.GetAllDomains();

            // Convert to JSON
            WriteJson(response, HttpStatusCode.OK, domains);
        }

        // === GET /api/domains/{id} ===
        public void GetById(HttpListenerResponse response, int id)
        {
            var domain = _dnService.GetDomainById(id);
            if (domain == null)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            // Convert to JSON
            WriteJson(response, HttpStatusCode.OK, domain);
        }

        // === POST /api/domains ===
        public void Create(HttpListenerResponse response, HttpListenerRequest request)
        {
            // Checks for Auth first
            if (!AuthHelper.RequireAuth(request, response)) return;

            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            string body = reader.ReadToEnd();
            
            Domain domain;

            try
            {
                domain = JsonSerializer.Deserialize<Domain>(body);
            }
            catch (JsonException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            bool succes = _dnService.CreateDomain(domain);
            if (succes)
            {
                response.StatusCode = (int)HttpStatusCode.Created;
                WriteJson(response, HttpStatusCode.Created, domain);
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        // === PUT /api/domains/{id} ===
        public void Update(HttpListenerResponse response, HttpListenerRequest request, int id)
        {
            // Checks for Auth first
            if (!AuthHelper.RequireAuth(request, response)) return;

            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            string body = reader.ReadToEnd();

            Domain domain;
            
            try
            {
                domain = JsonSerializer.Deserialize<Domain>(body);
                domain.DomainId = id; // Ensure the ID is set for the update
            }
            catch (JsonException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            bool success = _dnService.UpdateDomain(domain);
            if (success)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                WriteJson(response, HttpStatusCode.OK, domain);
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        // === DELETE /api/domains/{id} ===
        public void Delete(HttpListenerRequest request, HttpListenerResponse response, int id)
        {
            // Checks for Auth first
            if (!AuthHelper.RequireAuth(request, response)) return;

            bool success = _dnService.DeleteDomain(id);

            if (!success)
            {
                WriteJson(response, HttpStatusCode.InternalServerError, new { message = "Domain not found or delete failed." });
                return;
            }

            WriteJson(response, HttpStatusCode.OK, new { message = "Domain deleted" });
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
