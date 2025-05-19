using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge_Center
{
    public class DomainService
    {
        private readonly Database _database;
        public DomainService(Database database) 
        {
            _database = database;
        }

        public bool CreateDomain(Domain domain);
        public List<Domain> GetAllDomains();
        public Domain GetDomainById(int domainId);
        public bool UpdateDomain(Domain domain);
        public bool DeleteDomain(int domainId);
    }
}
