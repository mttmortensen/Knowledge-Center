using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Knowledge_Center
{
    public class DomainService
    {
        private readonly Database _database;
        public DomainService(Database database) 
        {
            _database = database;
        }

        /* ===================== CRUD ===================== */

        // === CREATE ===
        public bool CreateDomain(Domain domain) 
        {
            // Set timestamps first
            DateTime now = DateTime.Now;
            domain.CreatedAt = now;
            domain.LastUsed = now;

            // Build SQL Parameters
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@DomainName", domain.DomainName),
                new SqlParameter("@DomainDescription", domain.DomainDescription),
                new SqlParameter("@DomainStatus", domain.DomainStatus),
                new SqlParameter("@CreatedAt", domain.CreatedAt),
                new SqlParameter("@LastUsed", domain.LastUsed)
            };

            // Run the INSERT query
            int result = _database.ExecuteNonQuery(DomainQueries.InsertDomain, parameters);

            // Return true to see if INSERT was successful
            return result > 0;
        }

        // === READ ===
        public List<Domain> GetAllDomains()
        {
            List<Domain> domains = new List<Domain>();

            var rawDBResults = _database.ExecuteQuery(DomainQueries.GetAllDomains, null);

            foreach (var rawDBRow in rawDBResults)
            {
                domains.Add(ConvertDBRowToClassObj(rawDBRow));
            }

            return domains;
        }

        public Domain GetDomainById(int domainId);

        // === UPDATE ===
        public bool UpdateDomain(Domain domain);

        // === DELETE ===
        public bool DeleteDomain(int domainId);

        /* ===================== DATA TYPE CONVERTERS (MAPPERS) ===================== */

        private Domain ConvertDBRowToClassObj(Dictionary<string, object> rawDBRow)
        {
            return new Domain
            {
                DomainId = Convert.ToInt32(rawDBRow["DomainId"]),
                DomainName = rawDBRow["DomainName"].ToString(),
                DomainDescription = rawDBRow["DomainDescription"].ToString(),
                DomainStatus = rawDBRow["DomainStatus"].ToString(),
                CreatedAt = Convert.ToDateTime(rawDBRow["CreatedAt"]),
                LastUsed = Convert.ToDateTime(rawDBRow["LastUsed"])
            };
        }
    }
}
