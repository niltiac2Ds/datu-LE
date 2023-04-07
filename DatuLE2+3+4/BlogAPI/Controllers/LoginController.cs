using BlogDataLibrary.Data;
using BlogDataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private ISqlData _db;
        public LoginController(IConfiguration config, ISqlData db)
        {
            _config = config;
            _db = db;
        }

        // Generate Session Token for Authentication
        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string userIdStr = user.Id.ToString();

            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, userIdStr),
                };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Allow Anonymous users (without Authentication)
        [AllowAnonymous]
        [HttpPost]
        [Route("/api/[controller]/login")]
        public ActionResult Login([FromBody] UserLogin login)
        {
            UserModel user = _db.Authenticate(login.UserName, login.Password);


            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            return NotFound("User Not Found");

        }



    }
}
