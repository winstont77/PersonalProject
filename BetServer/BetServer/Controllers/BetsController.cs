using BetServer.Data;
using BetServer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;
using System.Data;
using System.Threading.Channels;
using RabbitMQ.Client;

namespace BetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {

        private readonly IModel _channel;

        public BetsController(IModel channel)
        {
            _channel = channel;
        }
        [HttpPost]
        public IActionResult PlaceBet([FromBody] string bet)
        {
            var body = Encoding.UTF8.GetBytes(bet);
            _channel.BasicPublish(exchange: "",
                                  routingKey: "bet_queue",
                                  basicProperties: null,
                                  body: body);

            return Ok("Bet placed successfully!");
        }

        public class Test
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        [Route("/TestPostBets")]
        [HttpPost]
        public async Task<IActionResult> TestPostBets([FromBody] Test test)
        {
            try
            {
                // Serialize the 'test' object to a JSON string using System.Text.Json
                var jsonString = JsonSerializer.Serialize(test);

                // Convert the JSON string to a byte array
                var body = Encoding.UTF8.GetBytes(jsonString);

                _channel.BasicPublish(exchange: "bet_exchange",
                                      routingKey: "routingKey1",
                                      basicProperties: null,
                                      body: body);
                Console.WriteLine("received");
                return Ok("received");
            }
            catch (Exception ex)
            {
                // Handle or log the exception as necessary
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

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
            using (var transaction = await db.Database.BeginTransactionAsync(IsolationLevel.Serializable))
            {
                try
                {

                    //var user = db.Users.Include(u => u.Bets).FirstOrDefault(u => u.Name == betRequest.UserName);
                    var user = db.Users.FromSqlRaw("SELECT * FROM Users WITH (UPDLOCK, ROWLOCK) WHERE Name = {0}", betRequest.UserName)
                   .Include(u => u.Bets)
                   .FirstOrDefault();

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

        //K6 press test
        public class Test2
        {
            public string UserName { get; set; }
        }
        [Authorize]
        [HttpPost]
        [Route("/TestPostBets")]
        public async Task<IActionResult> TestPostBets(DemoDBContext db, [FromBody] Test2 test)
        {
            using (var transaction = await db.Database.BeginTransactionAsync(IsolationLevel.Serializable))
            {
                try
                {
                    var user = db.Users.FromSqlRaw("SELECT * FROM Users WITH (UPDLOCK, ROWLOCK) WHERE Name = {0}", "w")
                   .Include(u => u.Bets)
                   .FirstOrDefault();

                    var betRequest = new BetRequest
                    {
                        Bet = new Bet
                        {
                            EventId = 1,
                            DateTime = "",
                            Money = 5000,
                            AwayTeamName = "Phoenix Suns",
                            HomeTeamName = "Golden State Warriors",
                            AwayTeamOdds = (float)(double)db.Events.FirstOrDefaultAsync(
                            e => e.AwayTeamName == "Phoenix Suns" &&
                            e.HomeTeamName == "Golden State Warriors"
                            ).Result.AwayTeamOdds,
                            HomeTeamOdds = (float)(double)db.Events.FirstOrDefaultAsync(
                            e => e.AwayTeamName == "Phoenix Suns" &&
                            e.HomeTeamName == "Golden State Warriors"
                            ).Result.HomeTeamOdds,
                            Sports = "basketball",
                            BetTeamName = "Phoenix Suns",
                            BetTeamOdds = (float)(double)db.Events.FirstOrDefaultAsync(
                            e => e.AwayTeamName == "Phoenix Suns" &&
                            e.HomeTeamName == "Golden State Warriors"
                            ).Result.AwayTeamOdds,
                            BetStatus = false,
                            UserId = 1,
                            CloseEvent = false,
                        },
                        UserName = test.UserName,
                        EventId = 2,
                        Prime = "",
                    };


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
    }
}
