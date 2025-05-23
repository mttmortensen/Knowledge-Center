using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Models;
using Knowledge_Center.Services;

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

        // === GET /api/logs/{id} ===

        // === POST /api/logs ===
    }
}
