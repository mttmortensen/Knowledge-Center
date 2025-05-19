using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge_Center
{
    public static class DomainQueries
    {
        public static readonly string InsertDomain = @"
            INSERT INTO Domains 
                (DomainName, DomainDescription, DomainStatus, CreatedAt, LastUsed)
            VALUES 
                (@DomainName, @DomainDescription, @DomainStatus, @CreatedAt, @LastUsed);
        ";

        public static readonly string GetAllDomains = @"
            SELECT * FROM Domains;
        ";

        public static readonly string GetDomainById = @"
            SELECT * FROM Domains
            WHERE 
                DomainId = @DomainId;
        ";

        public static readonly string UpdateDomain = @"
            UPDATE Domains
            SET 
                DomainName = @DomainName,
                DomainDescription = @DomainDescription,
                DomainStatus = @DomainStatus,
                CreatedAt = @CreatedAt,
                LastUsed = @LastUsed
            WHERE 
                DomainId = @DomainId;
        ";

        public static readonly string DeleteDomain = @"
            DELETE FROM Domains
            WHERE 
                DomainId = @DomainId;
        ";


    }
}
