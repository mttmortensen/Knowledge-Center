CREATE TABLE KnowledgeNodes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    NodeType NVARCHAR(50) NOT NULL DEFAULT 'Concept',
    DomainId INT NOT NULL,
    Description NVARCHAR(MAX),
    ConfidenceLevel INT,
    Status NVARCHAR(25),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    LastUpdated DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (DomainId) REFERENCES Domains(DomainId)
);