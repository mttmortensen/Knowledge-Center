using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knowledge_Center.Services;

namespace Knowledge_Center.API.Controllers
{
    public class TagController
    {
        private readonly TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        // === GET /api/tags ===
        // === GET /api/tags/{id} ===
        // === POST /api/tags ===
        // === PUT /api/tags/{id} ===
        // === DELETE /api/tags/{id} ===

        // === HELPER ===
    }
}
