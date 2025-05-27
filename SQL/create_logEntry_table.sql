CREATE TABLE LogEntries (
    LogId INT IDENTITY(1,1) PRIMARY KEY,
    NodeId INT NOT NULL,
    EntryDate DATETIME NOT NULL DEFAULT GETDATE(),
    Content NVARCHAR(MAX),
    TagId INT NOT NULL,
    ContributesToProgress BIT NOT NULL,
    FOREIGN KEY (TagId) REFERENCES Tags(TagId),
    FOREIGN KEY (NodeId) REFERENCES KnowledgeNodes(Id)
);
