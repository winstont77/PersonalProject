using BetServer.Data;
using BetServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            //if (eventEntity.AwayTeamPoint < eventEntity.HomeTeamPoint)
            //{
            //    if (eventEntity.AwayTeamScore - eventEntity.HomeTeamScore > Math.Abs(eventEntity.HomeTeamPoint)|| eventEntity.AwayTeamScore < eventEntity.HomeTeamScore)
            //    {
            //        eventEntity.Point = true;
            //    }
            //    else
            //    {
            //        eventEntity.Point = false;
            //    }
            //}
            //else
            //{
            //    if (eventEntity.HomeTeamScore - eventEntity.AwayTeamScore > Math.Abs(eventEntity.HomeTeamPoint)|| eventEntity.HomeTeamScore < eventEntity.AwayTeamScore)
            //    {
            //        //大於讓分
            //        eventEntity.Point = true;
            //    }else
            //    {
            //        //和局
            //        eventEntity.Point = false;
            //    }
            //}
            if ((eventEntity.AwayTeamScore - eventEntity.HomeTeamScore ) > 0)
            {
                if(eventEntity.AwayTeamPoint > 0)
                {
                    eventEntity.Point = true;
                }else if(eventEntity.AwayTeamPoint < 0 && (eventEntity.AwayTeamScore - eventEntity.HomeTeamScore) > Math.Abs((int)eventEntity.AwayTeamPoint))
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

            if (betsForEvent.ToList()[0].EventId!=null)
            {
                foreach (var user in db.Users)
                {
                    if (user.Bets!=null)
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

            db.SaveChanges();

            return Ok(betsForEvent.ToList());
        }
    }
}
