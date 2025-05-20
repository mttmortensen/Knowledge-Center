using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

            // Set timestamps
            DateTime now = DateTime.Now;
            log.EntryDate = now;

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@NodeId", log.NodeId),
                new SqlParameter("@EntryDate", log.EntryDate),
                new SqlParameter("@Content", log.Content),
                new SqlParameter("@Tag", log.Tag),
                new SqlParameter("@ContributesToProgress", log.ContributesToProgress)
            };

            // Run the INSERT query
            int result = _database.ExecuteNonQuery(LogEntryQueries.InsertLogEntry, parameters);

            // Return true to see if INSERT was successful
            return result > 0;
        }

        // === READ ===
        public List<LogEntry> GetAllLogEntriesByNodeId(int nodeId) 
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@NodeId", nodeId)
            };

            // SELECT Query + Parameters to retrieve all LogEntries from a specific Knowledge Node and maps results into LogEntry objects

            var rawDBResults = _database.ExecuteQuery(LogEntryQueries.GetLogsByNodeId, parameters);

            if (rawDBResults.Count == 0)
            {
                return null;
            }

            List<LogEntry> logEntries = new List<LogEntry>();

            foreach (var rawDBRow in rawDBResults)
            {
                logEntries.Add(ConvertDBRowToClassObj(rawDBRow));
            }

            return logEntries;
        }

        // This function won't have any use in the current version of the app, but it's here for future use
        public LogEntry GetLogEntryByLogId(int logId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@LogId", logId)
            };
            // SELECT Query + Parameters to retrieve a specific LogEntry by LogID and map result into a LogEntry object
            var rawDBResults = _database.ExecuteQuery(LogEntryQueries.GetLogByLogId, parameters);
            if (rawDBResults.Count == 0)
            {
                return null;
            }
            return ConvertDBRowToClassObj(rawDBResults[0]);
        }

        // === DELETE ===
        public bool DeleteAllLogEntriesByNodeId(int nodeId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@NodeId", nodeId)
            };

            // DELETE Query + Parameters to delete all LogEntries from a specific Knowledge Node
            int result = _database.ExecuteNonQuery(LogEntryQueries.DeleteAllLogsByNodeId, parameters);

            // Return true to see if DELETE was successful
            return result > 0;
        }

        /* ===================== DATA TYPE CONVERTERS (MAPPERS) ===================== */

        private LogEntry ConvertDBRowToClassObj(Dictionary<string, object> rawDBRow)
        {
            return new LogEntry
            {
                NodeId = Convert.ToInt32(rawDBRow["NodeId"]),
                EntryDate = Convert.ToDateTime(rawDBRow["EntryDate"]),
                Content = rawDBRow["Content"].ToString(),
                Tag = rawDBRow["Tag"].ToString(),
                ContributesToProgress = (bool)rawDBRow["ContributesToProgress"]
            };
        }
    }
}
