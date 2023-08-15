using BetServer.Data;
using BetServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        [Route("/PostUsers")]
        public async Task<IActionResult> PostUsers(DemoDBContext db, [FromBody] User user)
        {
            try
            {
                //db.Add<User>(user);
                //db.SaveChanges();
                return Ok(user);
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
