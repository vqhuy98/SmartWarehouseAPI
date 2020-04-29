using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SWHRestApiCore.Models;

namespace SWHRestApiCore.Controllers
{
    [Route("api/loginController")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private static string SECRETKEY = "SWHDatabaseSE1268";
        [HttpPost("login")]
        public string login([FromBody] LoginViewModel model)
        {
            // set username password trong db
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.Default.GetBytes(SECRETKEY);
            var claim = new Claim(ClaimTypes.NameIdentifier, "1");
            var claim2 = new Claim(ClaimTypes.Name, model.username);
            var claim3 = new Claim("Password", model.password);
            var claim4 = new Claim(ClaimTypes.Role,"staff");
            List<Claim> claims = new List<Claim>() { claim, claim2, claim3, claim4 };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "SWH",
                Audience = "SWH",
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="staff")]
        [HttpGet("hihi")]
        public String testAuthorize()
        {
            return "success";
        }
    }
}