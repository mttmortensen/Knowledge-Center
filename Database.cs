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

        // === Executing Read Operations (SELECT) ===


    }
}
