using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using static BetServer.Controllers.BetsController;
using System.Text;
using System.Text.Json;

namespace BetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestQueueController : ControllerBase
    {
        private readonly IModel _channel;

        public TestQueueController(IModel channel)
        {
            _channel = channel;
        }
        public class TestRoute
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        [HttpPost]
        [Route("/TestRouteKey2")]
        public async Task<IActionResult> TestRouteKey2([FromBody] TestRoute testRoute)
        {
            try
            {
                // Serialize the 'test' object to a JSON string using System.Text.Json
                var jsonString = JsonSerializer.Serialize(testRoute);

                // Convert the JSON string to a byte array
                var body = Encoding.UTF8.GetBytes(jsonString);

                _channel.BasicPublish(exchange: "bet_exchange",
                                      routingKey: "routingKey2",
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
    }
}
