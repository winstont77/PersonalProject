﻿using BetServer.Data;
using BetServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportDataController : ControllerBase
    {
        [HttpGet]
        [Route("/GetSoccer")]
        public async Task<IActionResult> GetSoccer()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://odds.p.rapidapi.com/v4/sports/soccer/odds?regions=us&oddsFormat=decimal&markets=h2h&dateFormat=iso"),
                Headers =
                {
                    { "X-RapidAPI-Key", "f55cb06f51mshb6a086cd7f7c47fp136fa1jsn695f0499478f" },
                    { "X-RapidAPI-Host", "odds.p.rapidapi.com" },
                },
            };
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);
                    return Ok(body);
                }
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

        [HttpGet]
        [Route("/GetFootball")]
        public async Task<IActionResult> GetFootball()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://odds.p.rapidapi.com/v4/sports/americanfootball_nfl/odds?regions=us&oddsFormat=decimal&markets=h2h&dateFormat=iso"),
                Headers =
                {
                    { "X-RapidAPI-Key", "f55cb06f51mshb6a086cd7f7c47fp136fa1jsn695f0499478f" },
                    { "X-RapidAPI-Host", "odds.p.rapidapi.com" },
                },
            };
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);
                    return Ok(body);
                }
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

        [HttpGet]
        [Route("/GetBasketball")]
        public async Task<IActionResult> GetBasketball() 
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://odds.p.rapidapi.com/v4/sports/basketball_nba/odds?regions=us&oddsFormat=decimal&markets=h2h&dateFormat=iso"),
                Headers =
                {
                    { "X-RapidAPI-Key", "f55cb06f51mshb6a086cd7f7c47fp136fa1jsn695f0499478f" },
                    { "X-RapidAPI-Host", "odds.p.rapidapi.com" },
                },
            };
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);
                    return Ok(body);
                }
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

        [HttpGet]
        [Route("/GetBaseball")]
        public async Task<IActionResult> GetBaseBall() 
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://odds.p.rapidapi.com/v4/sports/baseball_mlb/odds?regions=us&oddsFormat=decimal&markets=h2h%2Cspreads&dateFormat=iso"),
                Headers =
                {
                    { "X-RapidAPI-Key", "f55cb06f51mshb6a086cd7f7c47fp136fa1jsn695f0499478f" },
                    { "X-RapidAPI-Host", "odds.p.rapidapi.com" },
                },
            };
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);
                    return Ok(body);
                }
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

        [HttpPost]
        [Route("/PostSports")]
        public async Task<IActionResult> PostSports(DemoDBContext db, [FromBody] Team team)
        {
            return Ok(team);
        }
    }
}
