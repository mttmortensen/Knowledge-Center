CREATE TABLE KnowledgeNodes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Domain NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    ConfidenceLevel INT,
    Status NVARCHAR(50),
    CreatedAt DATETIME NOT NULL,
    LastUpdated DATETIME NOT NULL
);