using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Knowledge_Center.Models;

namespace Knowledge_Center.API.Controllers
{
    public class LoginController
    {
        public void HandleLogin(HttpListenerResponse response, HttpListenerRequest request) 
        {
            // Step 1: Read Body
            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            string body = reader.ReadToEnd();

            // Step 2: Parse JSON
            var loginData = JsonSerializer.Deserialize<LoginRequest>(body);
        }
        public void HandleLogout(HttpListenerResponse response, HttpListenerRequest request) { }

    }
}
