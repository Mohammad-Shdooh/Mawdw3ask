using BAL.Interfaces;
using Entity.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class AuthController : Controller
    {
        public IConfiguration configuration;
        private readonly IUserService userService;
        private UserResponse CheckUser;

        public AuthController(IConfiguration configuration,IUserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("SignIn")]
        public UserResponse Auth([FromBody] logInRequest user)
        {

            string response = "Unauthorized";
            if (user != null)
            {
                this.CheckUser = userService.SignIn(user);
                if (this.CheckUser != null)
                {
                    this.CheckUser.Token = this.Token();

                    return this.CheckUser;

                }
            }
            return this.CheckUser;
        }
        [HttpGet("RefreshToken")]
        public string RefreshToken()
        {
            string token = Request.Headers["Authorization"];
            if (token != null)
            {
                return this.Token();
            }
            else return null;
        }
        public string Token()
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );

            var subject = new ClaimsIdentity(new[]
                {
                        new Claim("title","Token")
                });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
