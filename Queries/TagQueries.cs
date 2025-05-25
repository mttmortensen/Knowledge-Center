using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge_Center.Queries
{
    public static class TagQueries
    {
        public static readonly string InsertTag = @"
            INSERT INTO Tags 
                (TName)
            VALUES 
                (@TName);
        ";
    }
}
