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

        // === READ ===
        public List<KnowledgeNode> GetAllNodes()
        {
            // SELECT Query + Parameters to retrieve all KnowledgeNodes and map results into KnowledgeNode objects
            throw new NotImplementedException("GetAllNodes method not implemented");
        }

        public KnowledgeNode GetNodeById(int id)
        {
            // SELECT Query + Parameters to retrieve a specific KnowledgeNode by ID and map result into a KnowledgeNode object
            throw new NotImplementedException("GetNodeById method not implemented");
        }

        // === UPDATE ===
        public bool UpdateNode(KnowledgeNode node)
        {
            // UPDATE Query + Parameters to update an existing KnowledgeNode by it's ID
            throw new NotImplementedException("UpdateNode method not implemented");
        }

        // === DELETE ===
        public bool DeleteNode(int id)
        {
            // DELETE Query + Parameters to delete a KnowledgeNode by ID
            throw new NotImplementedException("DeleteNode method not implemented");
        }
    }
}
