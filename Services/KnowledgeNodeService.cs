﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Knowledge_Center.Models;

namespace Knowledge_Center.Services
{
    public class KnowledgeNodeService
    {
        private readonly Database _database;

        public KnowledgeNodeService(Database database)
        {
            _database = database;
        }

        /* ===================== CRUD ===================== */

        // === CREATE ===
        public bool CreateNode(KnowledgeNode node) 
        {
            // Set timestamps
            DateTime now = DateTime.Now;
            node.CreatedAt = now;
            node.LastUpdated = now;

            // Build SQL Parameters
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Title", node.Title),
                new SqlParameter("@DomainId", node.DomainId),
                new SqlParameter("@Description", node.Description),
                new SqlParameter("@ConfidenceLevel", node.ConfidenceLevel),
                new SqlParameter("@Status", node.Status),
                new SqlParameter("@CreatedAt", node.CreatedAt),
                new SqlParameter("@LastUpdated", node.LastUpdated),
                new SqlParameter("@NodeType", node.NodeType)
            };

            // Run the INSERT query
            int result = _database.ExecuteNonQuery(KnowledgeNodeQueries.InsertNode, parameters);

            // Return true to see if INSERT was successful
            return result > 0;
        }

        // === READ ===
        public List<KnowledgeNode> GetAllNodes()
        {
            // SELECT Query + Parameters to retrieve all KnowledgeNodes and map results into KnowledgeNode objects
            List<KnowledgeNode> nodes = new List<KnowledgeNode>();

            var rawDBResults = _database.ExecuteQuery(KnowledgeNodeQueries.SelectAllNodes, null);

            foreach (var rawDBRow in rawDBResults) 
            {
                nodes.Add(ConvertDBRowToClassObj(rawDBRow));
            }

            return nodes;
        }

        public KnowledgeNode GetNodeById(int id)
        {
            // SELECT Query + Parameters to retrieve a specific KnowledgeNode by ID and map result into a KnowledgeNode object
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id )
            };

            var rawDBResults = _database.ExecuteQuery(KnowledgeNodeQueries.SelectNodeById, parameters);

            if (rawDBResults.Count == 0)
            {
                return null; // No node found with the given ID
            }

            return ConvertDBRowToClassObj(rawDBResults[0]);
        }

        // === UPDATE ===
        public bool UpdateNode(KnowledgeNode node)
        {
            // UPDATE Query + Parameters to update an existing KnowledgeNode by it's ID
            node.LastUpdated = DateTime.Now;

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", node.Id),
                new SqlParameter("@Title", node.Title),
                new SqlParameter("@DomainId", node.DomainId),
                new SqlParameter("@NodeType", node.NodeType),
                new SqlParameter("@Description", node.Description),
                new SqlParameter("@ConfidenceLevel", node.ConfidenceLevel),
                new SqlParameter("@Status", node.Status),
                new SqlParameter("@LastUpdated", node.LastUpdated)
            };

            int result = _database.ExecuteNonQuery(KnowledgeNodeQueries.UpdateNode, parameters);

            // Return true to see if UPDATE was successful
            return result > 0;
        }

        // === DELETE ===
        public bool DeleteNode(int id)
        {
            // DELETE Query + Parameters to delete a KnowledgeNode by ID
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            int result = _database.ExecuteNonQuery(KnowledgeNodeQueries.DeleteNode, parameters);

            // Return true to see if DELETE was successful
            return result > 0;
        }

        /* ===================== DATA TYPE CONVERTERS (MAPPERS) ===================== */
        private KnowledgeNode ConvertDBRowToClassObj(Dictionary<string, object> rawDBRow)
        {
            return new KnowledgeNode
            {
                Id = Convert.ToInt32(rawDBRow["Id"]),
                Title = rawDBRow["Title"].ToString(),
                DomainId = Convert.ToInt32(rawDBRow["DomainId"]),
                NodeType = rawDBRow["NodeType"].ToString(),
                Description = rawDBRow["Description"].ToString(),
                ConfidenceLevel = Convert.ToInt32(rawDBRow["ConfidenceLevel"]),
                Status = rawDBRow["Status"].ToString(),
                CreatedAt = Convert.ToDateTime(rawDBRow["CreatedAt"]),
                LastUpdated = Convert.ToDateTime(rawDBRow["LastUpdated"])
            };
        }
    }
}
