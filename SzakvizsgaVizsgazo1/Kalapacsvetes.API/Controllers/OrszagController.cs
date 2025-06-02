using KozosKonyvtar.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kalapacsvetes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrszagController : GenericController<Orszag>
    {
        public OrszagController(ApplicationDbContext context) : base(context) { }
    }
}
