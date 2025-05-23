using System;
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

        // === POST /api/domains ===

        // === PUT /api/domains/{id} ===

        // === DELETE /api/domains/{id} ===

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
