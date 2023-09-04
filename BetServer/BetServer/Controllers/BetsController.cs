using BetServer.Data;
using BetServer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;

namespace BetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {

        public class BetRequest
        {
            public Bet Bet { get; set; }
            public string UserName { get; set; }
            public int EventId { get; set; }
            public string Prime { get; set; }
        }

        [Authorize]
        [HttpPost]
        [Route("/PostBets")]
        public async Task<IActionResult> PostBets(DemoDBContext db, [FromBody] BetRequest betRequest) 
        {
            using (var transaction = await db.Database.BeginTransactionAsync())
            {
                try
                {

                    var user = db.Users.Include(u => u.Bets).FirstOrDefault(u => u.Name == betRequest.UserName);
                    // var user = db.Users.FromSqlRaw("SELECT * FROM Users WITH (UPDLOCK, ROWLOCK) WHERE Name = {0}", betRequest.UserName)
                    //.Include(u => u.Bets)
                    //.FirstOrDefault();

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



                            if (jsonContent.GetProperty("msg").ToString() == "Success")
                            {
                                betRequest.Bet.BetStatus = true;
                            }
                            else
                            {
                                betRequest.Bet.BetStatus = false;
                            }


                            user.Money = user.Money + betRequest.Bet.Money;
                            db.SaveChanges();

                            user.Bets.Add(betRequest.Bet);
                            db.SaveChanges();


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


                            await transaction.CommitAsync();
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
                    await transaction.RollbackAsync();
                    return StatusCode(500, $"Database error: {dbEx.Message}");
                }
                catch (HttpRequestException ex) // 這裡捕獲了由 HttpClient 丟出的異常
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
                catch (Exception ex) // 這裡捕獲了其他不明確的異常
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        [HttpGet]
        [Route("/GetTestENV")]
        public async Task<IActionResult> GetTestENV()
        {
            return Ok("success");
        }
    }
}
