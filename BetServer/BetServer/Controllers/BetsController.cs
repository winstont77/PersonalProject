using BetServer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        [HttpPost]
        [Route("/PostBets")]
        public async Task<IActionResult> PostBets(DemoDBContext db) 
        {
            return Ok(Environment.GetEnvironmentVariable("STATIC_RESOURCE"));
        }

        [HttpGet]
        [Route("/GetTestENV")]
        public async Task<IActionResult> GetTestENV()
        {
            return Ok("success");
        }
    }
}
