using Azure.Core;
using Knowledge_Center.Models;
using Knowledge_Center.Services.Core;
using Knowledge_Center.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Knowledge_Center.API.Controllers
{
    public class TagController
    {
        private readonly TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        // === GET /api/tags ===
        public void GetAll(HttpListenerResponse response)
        {
            // Get All Tags
            List<Models.Tags> tags = _tagService.GetAllTags();
            
            // Convert to JSON
            WriteJson(response, HttpStatusCode.OK, tags);
        }

        // === GET /api/tags/{id} ===
        public void GetById(HttpListenerResponse response, int id)
        {
            var tag = _tagService.GetTagById(id);

            if (tag == null)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            // Convert to JSON
            WriteJson(response, HttpStatusCode.OK, tag);
        }

        // === POST /api/tags ===
        public void Create(HttpListenerResponse response, HttpListenerRequest request)
        {
            // Checks for Auth first
            if (!AuthHelper.RequireAuth(request, response)) return;

            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            string body = reader.ReadToEnd();

            Tags tag;
            
            try
            {
                tag = JsonSerializer.Deserialize<Models.Tags>(body);
            }
            catch (JsonException)
            {
                WriteJson(response, HttpStatusCode.BadRequest, new { message = "Invalid JSON format." });
                return;
            }

            bool success = _tagService.CreateTag(tag);
            if (success)
            {

                response.StatusCode = (int)HttpStatusCode.Created;
                WriteJson(response, HttpStatusCode.Created, tag);
            }
            else
            {
                WriteJson(response, HttpStatusCode.InternalServerError, new { message = "Failed to create Tag." });
            }

        }

        // === PUT /api/tags/{id} ===
        public void Update(HttpListenerResponse response, HttpListenerRequest request, int id)
        {
            // Checks for Auth first
            if (!AuthHelper.RequireAuth(request, response)) return;

            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            string body = reader.ReadToEnd();

            Tags tag;
            
            try
            {
                tag = JsonSerializer.Deserialize<Models.Tags>(body);
            }
            catch (JsonException)
            {
                WriteJson(response, HttpStatusCode.BadRequest, new { message = "Invalid JSON format." });
                return;
            }

            bool success = _tagService.UpdateTag(tag);
            if (success)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                WriteJson(response, HttpStatusCode.OK, tag);
            }
            else
            {
                WriteJson(response, HttpStatusCode.InternalServerError, new { message = "Failed to update Tag." });
            }
        }

        // === DELETE /api/tags/{id} ===
        public void Delete(HttpListenerRequest request, HttpListenerResponse response, int id)
        {
            // Checks for Auth first
            if (!AuthHelper.RequireAuth(request, response)) return;

            bool success = _tagService.DeleteTag(id);

            if (!success)
            {
                WriteJson(response, HttpStatusCode.NotFound, new { message = "Tag not found or delete failed." });
                return;
            }

            WriteJson(response, HttpStatusCode.NoContent, new { message = "Tag deleted" });
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
