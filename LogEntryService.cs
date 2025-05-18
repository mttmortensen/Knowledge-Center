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
        public bool CreateLogEntry(LogEntry log) 
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@NodeId", log.NodeId),
                new SqlParameter("@EntryDate", log.EntryDate),
                new SqlParameter("@Content", log.Content),
                new SqlParameter("@Tags", log.Tags),
                new SqlParameter("@ContributesToProgress", log.ContributesToProgress)
            };

            // Run the INSERT query
            int result = _database.ExecuteNonQuery(LogEntryQueries.InsertLogEntry, parameters);

            // Return true to see if INSERT was successful
            return result > 0;
        }

        // === READ ===
        // === UPDATE ===
        // === DELETE ===

        /* ===================== DATA TYPE CONVERTERS (MAPPERS) ===================== */
    }
}
