using BetServer.Data;
using BetServer.Models;
using BetServer.Models.Function;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BetServer.Controllers
{
    public class BetRequest
    {
        public Bet Bet { get; set; }
        public int UserId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        [HttpPost]
        [Route("/PostBets")]
        public async Task<IActionResult> PostBets(DemoDBContext db, [FromBody] BetRequest betRequest)
        {
            try
            {

                var user = db.Users.Include(u => u.Bets).FirstOrDefault(u => u.Id == betRequest.UserId);

                user.Bets.Add(betRequest.Bet);
                db.SaveChanges();
                return Ok(betRequest);



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
