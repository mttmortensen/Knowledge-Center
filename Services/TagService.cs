using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Models;
using Knowledge_Center.Queries;
using Microsoft.Data.SqlClient;

namespace Knowledge_Center.Services
{
    public class TagService
    {
        private readonly Database _db;
        public TagService(Database db) 
        {
            _db = db;
        }

        /* ===================== CRUD ===================== */

        // Create
        public bool CreateTag(Tags tag) 
        {

            //Build SQL Parameters
            List<SqlParameter> tagParameters = new List<SqlParameter>
            {
                new SqlParameter("@TName", tag.Name)
            };

            // Run the INSERT Query
            int result = _db.ExecuteNonQuery(TagQueries.InsertTag, tagParameters);

            // Return true to see if INSERT was successful
            return result > 0;
        }

        // Read
        // Update
        // Delete

        /* ===================== DATA TYPE CONVERTERS (MAPPERS) ===================== */

    }
}
