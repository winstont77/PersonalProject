using BetServer.Data;
using BetServer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static BetServer.Controllers.UserController;

namespace BetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        [Route("/PostUsers")]
        public async Task<IActionResult> PostUsers(DemoDBContext db, [FromBody] User user)
        {
            try
            {
                // Check if a user with the same name already exists in the database
                var existingUser = db.Users.FirstOrDefault(u => u.Name == user.Name);
                if (existingUser != null)
                {
                    return BadRequest($"User with name {user.Name} already exists.");
                }

                db.Add<User>(user);
                await db.SaveChangesAsync();
                return Ok(user);
            }
            catch (DbUpdateException dbEx) // Capture EF's database exception
            {
                return StatusCode(500, $"Database error: {dbEx.Message}");
            }
            catch (HttpRequestException ex) // Here we capture the exception thrown by HttpClient
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            catch (Exception ex) // Here we capture other unspecified exceptions
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }



        public class SignIn
        {
            public string Name { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        [Route("/PostSignIn")]
        public async Task<IActionResult> PostSignIn(DemoDBContext db, [FromBody] SignIn signIn)
        {
            // Validate user credentials (this step is missing in your code)
            var user = await db.Users.FirstOrDefaultAsync(u => u.Name == signIn.Name && u.Password == signIn.Password);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }

            // STEP1: Create user claims
            var userClaims = new ClaimsIdentity(new[] {
                new Claim(JwtRegisteredClaimNames.Name, signIn.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("CustomClaim", "Anything You Like"),
            });

            // STEP2: Get SymmetricSecurityKey for JWT Signature
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // STEP3: Create JWT TokenHandler and TokenDescriptor
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                Subject = userClaims,
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };

            // Create JWT Token
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var serializeToken = tokenHandler.WriteToken(securityToken);

            // Return the token and user data
            return Ok(new
            {
                Token = serializeToken,
                Name = signIn.Name,
                Money = user.Money,
                Profit = user.Profit,
            });
        }

        public class MemberName
        {
            public string Name { get; set; }
        }

        [Authorize]
        [HttpPost]
        [Route("/PostUserDetail")]
        public async Task<IActionResult> PostUserDetail(DemoDBContext db, MemberName memberName)
        {
            if (memberName == null || string.IsNullOrWhiteSpace(memberName.Name))
            {
                return BadRequest("Invalid member name provided.");
            }
            try
            {
                // Fetch user details asynchronously
                var user = await db.Users.Include(u => u.Bets)
                                          .FirstOrDefaultAsync(u => u.Name == memberName.Name);

                // Check if user exists
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(new
                {
                    user.Name,
                    user.Money,
                    user.Profit,
                    user.Bets
                });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

        [Authorize]
        [HttpPost]
        [Route("/PostBetHistory")]
        public async Task<IActionResult> PostBetHistory(DemoDBContext demoDBContext, MemberName memberName)
        {

            return Ok();
        }
    }
}
