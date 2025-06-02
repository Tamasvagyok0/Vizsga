using KozosKonyvtar.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kalapacsvetes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportoloController : GenericController<Sportolo>
    {
        public SportoloController(ApplicationDbContext context) : base(context) { }
    }
}
