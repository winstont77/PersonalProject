using BetServer.Data;
using BetServer.Models;
using BetServer.Models.Function;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace BetServer.Controllers
{
    public class BetRequest
    {
        public Bet Bet { get; set; }
        public string UserName { get; set; }
        public int EventId { get; set; }
        public string Prime { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        private readonly IConfiguration _config;
        public BetsController(IConfiguration config)
        {
            _config = config;
        }

        [Authorize]
        [HttpPost]
        [Route("/PostBets")]
        public async Task<IActionResult> PostBets(DemoDBContext db, [FromBody] BetRequest betRequest)
        {
            try
            {

                var user = db.Users.Include(u => u.Bets).FirstOrDefault(u => u.Name == betRequest.UserName);

                //user.Bets.Add(betRequest.Bet);
                //db.SaveChanges();

                var tappay = new
                {
                    prime = betRequest.Prime,
                    partner_key = "partner_PHgswvYEk4QY6oy3n8X3CwiQCVQmv91ZcFoD5VrkGFXo8N7BFiLUxzeG",
                    merchant_id = "AppWorksSchool_CTBC",
                    details = "TapPay Test",
                    amount = betRequest.Bet.Money,
                    cardholder = new
                    {
                        phone_number = "+886923456789",
                        name = "Jane Doe",
                        email = "Jane@Doe.com",
                        zip_code = "12345",
                        address = "123 1st Avenue, City, Country",
                        national_id = "A123456789"
                    },
                    remember = true
                };


                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("x-api-key", "partner_PHgswvYEk4QY6oy3n8X3CwiQCVQmv91ZcFoD5VrkGFXo8N7BFiLUxzeG");
                    var content = new StringContent(JsonSerializer.Serialize(tappay), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("https://sandbox.tappaysdk.com/tpc/payment/pay-by-prime", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentStream = await response.Content.ReadAsStreamAsync();
                        var jsonContent = await JsonSerializer.DeserializeAsync<JsonElement>(contentStream);

                        //var tappays = new Tappay
                        //{
                        //    Amount = jsonContent.GetProperty("amount").GetUInt16(),
                        //    Msg = jsonContent.GetProperty("msg").ToString(),
                        //    Prime = betRequest.Prime
                        //};

                        //db.Tappays.Add(tappays);
                        //db.SaveChanges();

                        //return Ok(jsonContent);

                        if (jsonContent.GetProperty("msg").ToString() == "Success")
                        {
                            betRequest.Bet.BetStatus = true;
                        }
                        else
                        {
                            betRequest.Bet.BetStatus = false;
                        }

                        //var selectEvent = await db.Events.FirstOrDefaultAsync(e => e.Id == betRequest.EventId);
                        //if (betRequest.Bet.BetTeamName==selectEvent.AwayTeamName)
                        //{
                        //    selectEvent.AwayTeamMoney = selectEvent.AwayTeamMoney + betRequest.Bet.Money;
                        //}
                        //else
                        //{
                        //    selectEvent.HomeTeamMoney = selectEvent.HomeTeamMoney + betRequest.Bet.Money;
                        //}
                        //if (selectEvent.AwayTeamOdds<selectEvent.HomeTeamOdds)
                        //{
                        //    var awayTeamMoney = 
                        //}

                        user.Money = user.Money + betRequest.Bet.Money;
                        db.SaveChanges();

                        user.Bets.Add(betRequest.Bet);
                        db.SaveChanges();


                        //var events = await db.Events.FirstOrDefaultAsync(e => e.Id == betRequest.EventId);
                        //if (betRequest.Bet.BetTeamName == events.HomeTeamName)
                        //{
                        //    events.HomeTeamOdds = events.HomeTeamOdds - (float)(betRequest.Bet.Money / 1000000.0);
                        //}
                        //else
                        //{
                        //    events.AwayTeamOdds = events.AwayTeamOdds - (float)(betRequest.Bet.Money / 1000000.0);
                        //}
                        //db.SaveChanges();

                        //return Ok(events);

                        //return Ok(user.Bets);

                        var selectEvent = await db.Events.FirstOrDefaultAsync(e => e.Id == betRequest.EventId);
                        if (betRequest.Bet.BetTeamName == selectEvent.AwayTeamName)
                        {
                            selectEvent.AwayTeamMoney = selectEvent.AwayTeamMoney + betRequest.Bet.Money;
                        }
                        else
                        {
                            selectEvent.HomeTeamMoney = selectEvent.HomeTeamMoney + betRequest.Bet.Money;
                        }
                        db.SaveChanges();



                        if (betRequest.Bet.BetTeamName == selectEvent.AwayTeamName)
                        {
                            selectEvent.AwayTeamOdds = selectEvent.AwayTeamOdds - (float)betRequest.Bet.Money / 500000;
                            selectEvent.HomeTeamOdds = selectEvent.HomeTeamOdds + (float)betRequest.Bet.Money / 500000;
                        }
                        else
                        {
                            selectEvent.HomeTeamOdds = selectEvent.HomeTeamOdds - (float)betRequest.Bet.Money / 500000;
                            selectEvent.AwayTeamOdds = selectEvent.AwayTeamOdds + (float)betRequest.Bet.Money / 500000;
                        }
                        db.SaveChanges();




                        //if(selectEvent.AwayTeamMoney != 0 && selectEvent.HomeTeamMoney != 0 && selectEvent.AwayTeamMoney > 5000 && selectEvent.HomeTeamMoney > 50000)
                        //{
                        //    if (selectEvent.AwayTeamOdds < selectEvent.HomeTeamOdds || selectEvent.AwayTeamOdds == selectEvent.HomeTeamOdds)
                        //    {
                        //        var rightSide = 1 / (selectEvent.AwayTeamOdds - 1);
                        //        var leftSide = 1 * (selectEvent.HomeTeamOdds - 1);
                        //        float betmoneyProportion = 0;
                        //        betmoneyProportion = selectEvent.AwayTeamMoney / (selectEvent.HomeTeamMoney);
                        //        if (leftSide < betmoneyProportion && betmoneyProportion < rightSide)
                        //        {
                        //            if (betmoneyProportion - leftSide < 0.1)
                        //            {
                        //                selectEvent.HomeTeamOdds = selectEvent.HomeTeamOdds - (float)0.1;
                        //            }
                        //            if (rightSide - betmoneyProportion < 0.1)
                        //            {
                        //                selectEvent.AwayTeamOdds = selectEvent.AwayTeamOdds - (float)0.1;
                        //            }
                        //        }
                        //        //return Ok(betmoneyProportion);
                        //    }
                        //    else
                        //    {
                        //        var rightSide = 1 / (selectEvent.AwayTeamOdds - 1);
                        //        var leftSide = 1 * (selectEvent.HomeTeamOdds - 1);
                        //        float betmoneyProportion = 0;
                        //        betmoneyProportion = (selectEvent.HomeTeamMoney) / selectEvent.AwayTeamMoney;
                        //        if (leftSide < betmoneyProportion && betmoneyProportion < rightSide)
                        //        {
                        //            if (betmoneyProportion - leftSide < 0.1)
                        //            {
                        //                selectEvent.AwayTeamOdds = selectEvent.AwayTeamOdds - (float)0.1;
                        //            }
                        //            if (rightSide - betmoneyProportion < 0.1)
                        //            {
                        //                selectEvent.HomeTeamOdds = selectEvent.HomeTeamOdds - (float)0.1;
                        //            }
                        //        }
                        //    }
                        //}
                        
                        db.SaveChanges();
                        return Ok(selectEvent);
                    }
                    else
                    {
                        return null;
                    }
                }


                //return Ok(betRequest);
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
