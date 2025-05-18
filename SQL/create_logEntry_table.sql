CREATE TABLE LogEntries (
    LogId INT IDENTITY(1,1) PRIMARY KEY,
    NodeId INT NOT NULL,
    EntryDate DATETIME NOT NULL,
    Content NVARCHAR(MAX),
    Tag NVARCHAR(50),
    ContributesToProgress BIT NOT NULL,
    FOREIGN KEY (NodeId) REFERENCES KnowledgeNodes(Id)
);
