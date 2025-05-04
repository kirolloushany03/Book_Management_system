using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using realationshipss.security;
using realationshipss.DTOS.Email;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NuGet.Protocol;

namespace realationshipss.Controllers.Email_controller
{
    [ApiController]
    [Route("api/email")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> sendemail([FromBody] EmailRequestDto request)
        {
            if (string.IsNullOrEmpty(request.To))
                return BadRequest("recipient email is required");

            try
            {
                await _emailService.SendEmailAsync(
                        request.To,
                        request.Subject,
                        request.Body
                    );

                return Ok(new { message = "Email sent successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Failed to sedn email: --> {ex.ToString()}" });
            }
        }    
    }
}
