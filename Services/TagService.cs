using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Models;

namespace Knowledge_Center.Services
{
    public class TagService
    {
        private readonly Database _db;
        public TagService(Database db) 
        {
            _db = db;
        }
    }
}
