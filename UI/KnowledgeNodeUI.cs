using Knowledge_Center.Models;
using Knowledge_Center.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge_Center.UI
{
    public class KnowledgeNodeUI
    {
        private readonly KnowledgeNodeService _knService;
        private readonly LogEntryService _lgService;
        private readonly DomainService _dnService;


        public KnowledgeNodeUI(KnowledgeNodeService knService, LogEntryService lgService, DomainService dnService) 
        {
            _knService = knService;
            _lgService = lgService;
            _dnService = dnService;

        }


    }
}
