using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionOrdinateurs.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("get-admin")]

        public IActionResult teste()
        {
            return Ok("You are admin.");
        }

        [HttpGet]
        [Authorize(Roles = "general")]
        [Route("get-general")]
        public IActionResult GetGeneral()
        {
            return Ok("You are general.");
        }
    }
}