using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Models;
using Knowledge_Center.Services;
using System.Text.Json;

namespace Knowledge_Center.API.Controllers
{
    public class LogEntryController
    {
        private readonly LogEntryService _lgService;

        public LogEntryController(LogEntryService lgService)
        {
            _lgService = lgService;
        }

        // === GET /api/logs ===
        public void GetAll(HttpListenerResponse response)
        {
            //Get All Log Entries
            List<LogEntry> logEntries = _lgService.GetAllLogEntries();

            // Convert to JSON
            WriteJson(response, HttpStatusCode.OK, logEntries);
        }

        // === GET /api/logs/{id} ===
        public void GetById(HttpListenerResponse response, int id)
        {
            var log = _lgService.GetLogEntryByLogId(id);
            if (log == null)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
            // Convert to JSON
            WriteJson(response, HttpStatusCode.OK, log);
        }

        // === POST /api/logs ===
        public void Create(HttpListenerResponse response, HttpListenerRequest request) 
        {
            // Read the request body
            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            string body = reader.ReadToEnd();

            // Deserialize the JSON body into a LogEntry object
            LogEntry log;
            
            try
            {
                log = JsonSerializer.Deserialize<LogEntry>(body);
            }
            catch (JsonException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            // Create the log entry
            bool success = _lgService.CreateLogEntry(log);
            if (!success)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return;
            }

            // Convert to JSON
            WriteJson(response, HttpStatusCode.Created, log);
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
