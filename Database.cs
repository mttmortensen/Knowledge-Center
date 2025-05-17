using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Knowledge_Center
{
    public class Database
    {
        private readonly string _connectionString;

        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        // === Connection Management ===

        private SqlConnection OpenConnection() 
        {
            // Opens and returns a new SQL connection
            throw new NotImplementedException();
        }

        // === Executing Write Operations (INSERT, UPDATE, DELETE) ===

        public int ExecuteNonQuery(string query, List<SqlParameter> parameters) 
        {
            // Executes a non-query SQL command (INSERT, UPDATE, DELETE) and returns a count of affected rows
            throw new NotImplementedException();
        }

        // === Executing Read Operations (SELECT) ===

        public List<Dictionary<string, object>> ExecuteQuery(string sql, List<SqlParameter> parameters)
        {
            // Executes a SQL query and returns a list of db row data (as key/value pairs)
            throw new NotImplementedException();
        }

    }
}
