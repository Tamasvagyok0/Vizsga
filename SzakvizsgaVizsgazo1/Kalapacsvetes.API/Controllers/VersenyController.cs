using KozosKonyvtar.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kalapacsvetes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersenyController : GenericController<Verseny>
    {
        public VersenyController(ApplicationDbContext context) : base(context) { }
    }
}
