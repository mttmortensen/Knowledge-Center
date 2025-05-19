CREATE TABLE Domains (
    DomainId INT IDENTITY(1,1) PRIMARY KEY,
    DomainName NVARCHAR(100),
    DomainStatus NVARCHAR(50),
    DomainDescription NVARCHAR (MAX),
	CreatedAt DATETIME NOT NULL default GETDATE(),
	LastUsed DATETIME NOT NULL default GETDATE()
);
