using KozosKonyvtar.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kalapacsvetes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
                                            // Itt kapja mega tábla nevét
    public class HelyszinController : GenericController<Helyszin>
    {
        public HelyszinController(ApplicationDbContext context) : base(context) { }
    }
}
