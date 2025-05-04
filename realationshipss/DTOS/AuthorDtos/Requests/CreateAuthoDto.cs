using System.ComponentModel.DataAnnotations;

namespace realationshipss.DTOS.AuthorDtos.Requests
{
    public class CreateAuthorDto
    {
        [Required]
        [MaxLength(50)]
            public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
            public string LastName { get; set; } = null!;

        [MaxLength(100)]
            public string Email { get; set; } = null!;

        public DateTime BirthDate { get; set; }
    }
}
