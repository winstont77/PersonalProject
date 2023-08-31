using BetServer.Data;
using BetServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static BetServer.Controllers.UsersController;

namespace BetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _config;
        public UsersController(IConfiguration config)
        {
            _config = config;
        }


        [HttpPost]
        [Route("/PostUsers")]
        public async Task<IActionResult> PostUsers(DemoDBContext db, [FromBody] User user)
        {
            try
            {
                db.Add<User>(user);
                db.SaveChanges();
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

        public class SignIn
        {
            public string Name { get; set; }
            public string Password { get; set; }
        }


        [HttpPost]
        [Route("/PostSignIn")]
        public async Task<IActionResult> PostSignIn(DemoDBContext db, [FromBody] SignIn signIn)
        {
            //var user = Authenticate(db,signIn);
            //if (user != null)
            //{
            //    var token = GenerateToken(user);
            //    return Ok(new
            //    { 
            //         token = token
            //    });
            //}
            //else
            //{
            //    return Ok("user not found");
            //}

            ////return NotFound("user not found");
            ///
            // STEP0: 在產生 JWT Token 之前，可以依需求做身分驗證

            // STEP1: 建立使用者的 Claims 聲明，這會是 JWT Payload 的一部分
            var userClaims = new ClaimsIdentity(new[] {
            new Claim(JwtRegisteredClaimNames.Name, signIn.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("CustomClaim", "Anything You Like"),
        });
            // STEP2: 取得對稱式加密 JWT Signature 的金鑰
            // 這部分是選用，但此範例在 Startup.cs 中有設定 ValidateIssuerSigningKey = true 所以這裡必填
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            // STEP3: 建立 JWT TokenHandler 以及用於描述 JWT 的 TokenDescriptor
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                Subject = userClaims,
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };
            // 產出所需要的 JWT Token 物件
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            // 產出序列化的 JWT Token 字串
            var serializeToken = tokenHandler.WriteToken(securityToken);

            return Ok(new 
            {
                Token = serializeToken,
                Name = signIn.Name,
                Money = db.Users.FirstOrDefault(u => u.Name == signIn.Name).Money,
                Profit = db.Users.FirstOrDefault(u=>u.Name == signIn.Name).Profit,
            });
        }

        //// To generate token
        //private string GenerateToken(User user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier,user.Name),

        //    };
        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //        _config["Jwt:Audience"],
        //        claims,
        //        expires: DateTime.Now.AddMinutes(15),
        //        signingCredentials: credentials);


        //    return new JwtSecurityTokenHandler().WriteToken(token);

        //}

        ////To authenticate user
        //private User Authenticate(DemoDBContext db, SignIn signIn)
        //{
        //    var currentUser = db.Users.FirstOrDefault(x => x.Name ==
        //        signIn.Name && x.Password == signIn.Password);
        //    if (currentUser != null)
        //    {
        //        return currentUser;
        //    }
        //    return null;
        //}


        public class MemberName
        {
            public string Name { get; set; }
        }
        [Authorize]
        [HttpPost]
        [Route("/PostMemberDetail")]
        public async Task<IActionResult> PostMemberDetail(DemoDBContext db, [FromBody] MemberName memberName) 
        {
            var user = db.Users.Include(u => u.Bets).FirstOrDefault(u => u.Name == memberName.Name);
            return Ok(new
            {
                Name = user.Name,
                Money = user.Money,
                Profit = user.Profit,
                Bet = user.Bets
            });
        }

        [Authorize]
        [HttpGet]
        [Route("/GetSelectTeam")]
        public async Task<IActionResult> GetSelectTeam()
        {
            return Ok("");
        }
    }
}
