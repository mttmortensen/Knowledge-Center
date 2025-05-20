using System;
using System.Net;
using System.IO;
using System.Text;
using System.Text.Json;
using Knowledge_Center.Services;
using Knowledge_Center.Models;
using System.Security.Cryptography;

namespace Knowledge_Center.API
{
    public class CoreAPI
    {
        private readonly KnowledgeNodeService _knowledgeNodeService;
        private readonly LogEntryService _logEntryService;
        private readonly DomainService _domainService;

        private readonly HttpListener _listener;

        public CoreAPI(KnowledgeNodeService knowledgeNodeService, LogEntryService logEntryService, DomainService domainService)
        {
            _knowledgeNodeService = knowledgeNodeService;
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


    }
}
