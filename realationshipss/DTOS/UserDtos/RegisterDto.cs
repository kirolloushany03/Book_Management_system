using System.ComponentModel.DataAnnotations;

namespace realationshipss.DTOS.UserDtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        public string? Role { get; set; } = "User";
    }
}
