using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using realationshipss.data;
using realationshipss.DTOS.UserDtos;
using realationshipss.Entities;

namespace realationshipss.Controllers.security_controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly Datacontext _context;

        public RegisterController(Datacontext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var UserExisting = await _context.TbUser.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
            if (UserExisting != null) 
            {
                return Conflict("Email is already registered");
            }

            var HashingPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var NewUser = new User
            {
                Email = registerDto.Email,
                PasswordHash = HashingPassword,
                //EmailVerified = userdto.EmailVerified,
                //Role = registerDto.Role
                Role = "User" // this for now to be the default and then we can make it to be choosed
            };

            _context.TbUser.Add(NewUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registerd successfully!"});
        }
    }
}
