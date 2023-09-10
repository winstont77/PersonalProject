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

        public class FindEventIndex
        {
            public int Id { get; set; }
            public string WinTeamName { get; set; }
            public string LoseTeamName { get; set; }
            public int AwayTeamScore { get; set; }
            public int HomeTeamScore { get; set; }
            public Event Events { get; set; }
        }

        [HttpPost]
        [Route("/PostCloseEvent")]
        public async Task<IActionResult> PostCloseEvent(DemoDBContext db, [FromBody] FindEventIndex findEventIndex)
        {
            var eventEntity = await db.Events.FirstOrDefaultAsync(e => e.Id == findEventIndex.Id);
            eventEntity.CloseEvent = true;
            eventEntity.LoseTeamName = findEventIndex.LoseTeamName;
            eventEntity.WinTeamName = findEventIndex.WinTeamName;
            eventEntity.AwayTeamScore = findEventIndex.AwayTeamScore;
            eventEntity.HomeTeamScore = findEventIndex.HomeTeamScore;

            if ((eventEntity.AwayTeamScore - eventEntity.HomeTeamScore) > 0)
            {
                if (eventEntity.AwayTeamPoint > 0)
                {
                    eventEntity.Point = true;
                }
                else if (eventEntity.AwayTeamPoint < 0 && (eventEntity.AwayTeamScore - eventEntity.HomeTeamScore) > Math.Abs((int)eventEntity.AwayTeamPoint))
                {
                    eventEntity.Point = true;
                }
                else
                {
                    eventEntity.Point = false;
                }
            }
            else
            {
                if (eventEntity.HomeTeamPoint > 0)
                {
                    eventEntity.Point = true;
                }
                else if (eventEntity.HomeTeamPoint < 0 && (eventEntity.HomeTeamScore - eventEntity.AwayTeamScore) > Math.Abs((int)eventEntity.HomeTeamPoint))
                {
                    eventEntity.Point = true;
                }
                else
                {
                    eventEntity.Point = false;
                }
            }
            db.SaveChanges();




            var betsForEvent = await db.Bets.Where(b => b.EventId == findEventIndex.Id).ToListAsync();

            if (betsForEvent.ToList()[0].EventId != null)
            {
                foreach (var user in db.Users)
                {
                    if (user.Bets != null)
                    {
                        foreach (var bet in user.Bets)
                        {
                            if (bet.BetTeamName == findEventIndex.WinTeamName)
                            {
                                if (eventEntity.Point == true)
                                {
                                    var profit = bet.Money * bet.BetTeamOdds;
                                    user.Profit = user.Profit + (int)profit;
                                }
                            }
                            user.Money = user.Money - bet.Money;
                        }
                    }
                }
            }

            foreach (var item in betsForEvent)
            {
                item.CloseEvent = true;
            }

            db.SaveChanges();

            return Ok(betsForEvent.ToList());
        }
    }
}
