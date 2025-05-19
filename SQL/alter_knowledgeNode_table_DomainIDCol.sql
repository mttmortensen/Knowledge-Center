ALTER TABLE KnowledgeNodes
ADD DomainId INT NOT NULL DEFAULT 0; -- Use 0 for "unassigned" or later migrate
