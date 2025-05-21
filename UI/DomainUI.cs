using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Services;

namespace Knowledge_Center.UI
{
    public class DomainUI
    {
        private readonly KnowledgeNodeService _knService;
        private readonly LogEntryService _lgService;
        private readonly DomainService _dnService;
        public DomainUI(KnowledgeNodeService knService, LogEntryService lgService, DomainService dnService)
        {
            _knService = knService;
            _lgService = lgService;
            _dnService = dnService;
        }
        public void ShowDomainMenu()
        {
            // Implementation for domain menu
            Console.WriteLine("Domain Menu is not implemented yet.");
        }
    }
}
