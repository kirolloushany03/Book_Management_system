using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using realationshipss.data;
using realationshipss.DTOS.UserDtos;
using realationshipss.security;

//will do it agan 

namespace realationshipss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtProvider _JwtProvider;
        private readonly Datacontext _context;

        public AuthenticationController(JwtProvider JwtProvider, Datacontext context)
        {
            _JwtProvider = JwtProvider;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            var user = await _context.TbUser.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var IsPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!IsPasswordValid)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var token = _JwtProvider.CreateToken(user);

            return Ok(new { Token = token });
        }
    }
}
