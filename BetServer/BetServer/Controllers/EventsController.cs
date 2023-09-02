using BetServer.Data;
using BetServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpGet]
        [Route("/GetEvents")]
        public async Task<IActionResult> GetEvents(DemoDBContext db)
        {
            return Ok(db.Events);
        }

        [HttpPost]
        [Route("/PostEvents")]
        public async Task<IActionResult> PostEvents(DemoDBContext db, [FromBody] List<Event> events)
        {
            try
            {
                foreach (var item in events)
                {
                    db.Add<Event>(item);
                }
                db.SaveChanges();
                return Ok(db.Events);
                //return Ok(events);
            }
            catch (DbUpdateException dbEx) // 捕獲 EF 的資料庫異常
            {
                return StatusCode(500, $"Database error: {dbEx.Message}");
            }
            catch (HttpRequestException ex) // 這裡捕獲了由 HttpClient 丟出的異常
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            catch (Exception ex) // 這裡捕獲了其他不明確的異常
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

    }
}
