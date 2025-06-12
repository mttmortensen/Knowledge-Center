using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Knowledge_Center.Models;
using Knowledge_Center.Services.Security;

namespace Knowledge_Center.API.Controllers
{
    public class LoginController
    {
        public void HandleLogin(HttpListenerResponse response, HttpListenerRequest request) 
        {
            try 
            {
                // Step 1: Read Body
                using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
                string body = reader.ReadToEnd();

                // Step 2: Parse JSON
                var loginData = JsonSerializer.Deserialize<LoginRequest>(
                    body,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                // Step 3: Validate creds against hardcoded creds (for testing ONLY)
                string hardcodedUsername = "matt";
                string hardcodedPassword = "thisShouldntB3Hardcoded!!";

                if(loginData.Username == hardcodedUsername && loginData.Password == hardcodedPassword) 
                {
                    string token = AuthSession.CreateSession(hardcodedUsername);

                    var responseBody = JsonSerializer.Serialize(new { token });
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseBody);

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.ContentType = "application/json";
                    response.OutputStream.Write(buffer, 0, buffer.Length);

                }
                else 
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Invalid credentials.");
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }
            }
            catch 
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Login request failed.");
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            finally 
            {
                response.OutputStream.Close();
            }

        }
        public void HandleLogout(HttpListenerResponse response, HttpListenerRequest request) 
        {
            try 
            {
                // Step 1: Get the token from Authorization Header 
                string authHeader = request.Headers["Authorization"];

                if(string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ")) 
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    byte[] errorBytes = System.Text.Encoding.UTF8.GetBytes("Missing or invalid Authorization header.");
                    response.OutputStream.Write(errorBytes, 0, errorBytes.Length);
                    return;
                }

                string token = authHeader.Substring("Bearer ".Length);

                // Step 2: End the session (look up by token)
                string username = AuthSession.GetUsernameByToken(token);

                if (username == null) 
                {
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    byte[] errorBytes = System.Text.Encoding.UTF8.GetBytes("Invalid token.");
                    response.OutputStream.Write(errorBytes, 0, errorBytes.Length);
                    return;
                }

                AuthSession.EndSession(username);

                // Step 3: Return success
                response.StatusCode = (int)HttpStatusCode.OK;
                byte[] okBytes = System.Text.Encoding.UTF8.GetBytes("Logout successful.");
                response.OutputStream.Write(okBytes, 0, okBytes.Length);

            }
            catch 
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                byte[] errorBytes = System.Text.Encoding.UTF8.GetBytes("Logout failed.");
                response.OutputStream.Write(errorBytes, 0, errorBytes.Length);

            }
            finally 
            {
                response.OutputStream.Close();
            }
        }

    }
}
