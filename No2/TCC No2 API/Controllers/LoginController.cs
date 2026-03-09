using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TCC_No2_API.DTOs;
using TCC_No2_API.Services;

namespace TCC_No2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserAuthenService _service;

        public LoginController(IUserAuthenService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if(await _service.Register(request)) return Ok();
            return BadRequest("User already exists");
        }

        [HttpPost]
        public async Task<IActionResult> VerifyPassword(VerifyPasswordRequest request)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Login(RegisterRequest request)
        {
            var user = "";
            //_context.Users.FirstOrDefault(x => x.Username == request.Username);

            if (user == null)
                return Unauthorized();

            bool isValid = false;
            //    BCrypt.Net.BCrypt.Verify(
            //    request.Password,
            //    user.PasswordHash
            //);

            if (!isValid)
                return Unauthorized();

            //var token = GenerateJwtToken(user);

            return Ok(new
            {
                //token,
                //username = user.Username
            });
        }

        private string GenerateJwtToken(RegisterRequest request)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, request.Username)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("super_secret_key_123")
            );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
