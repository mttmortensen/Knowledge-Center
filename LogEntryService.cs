using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Knowledge_Center
{
    public class LogEntryService
    {
        private readonly Database _database;

        public LogEntryService(Database database)
        {
            _database = database;
        }

        /* ===================== CRUD ===================== */

        // === CREATE ===
        // === READ ===
        // === UPDATE ===
        // === DELETE ===

        /* ===================== DATA TYPE CONVERTERS (MAPPERS) ===================== */
    }
}
