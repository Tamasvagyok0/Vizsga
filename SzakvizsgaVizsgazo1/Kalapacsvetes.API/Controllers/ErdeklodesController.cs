using KozosKonyvtar.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kalapacsvetes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErdeklodesController : GenericController<Erdeklodes>
    {
        public ErdeklodesController(ApplicationDbContext context) : base(context) { }
    }
}

