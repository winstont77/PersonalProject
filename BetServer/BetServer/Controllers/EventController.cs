using BetServer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        [HttpGet]
        [Route("/GetEvents")]
        public async Task<IActionResult> GetEvents(DemoDBContext db)
        {
            return Ok(db.Events);
        }
    }
}
