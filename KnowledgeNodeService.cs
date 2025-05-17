using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge_Center
{
    public class KnowledgeNodeService
    {
        private readonly Database _database;

        public KnowledgeNodeService(Database database)
        {
            _database = database;
        }

        // === CREATE ===
        public bool CreateNode(KnowledgeNode node) 
        {
            // INSERT Query + Parameters to store new KnowledgeNode
            throw new NotImplementedException("CreateNode method not implemented");
        }
    }
}
