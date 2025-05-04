using System.ComponentModel.DataAnnotations;
using realationshipss.DTOS.BookAuthorDtos;

namespace realationshipss.DTOS.AuthorDtos.Responses
{
    public class AuthorDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
            public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
            public string LastName { get; set; } = null!;

        [MaxLength(100)]
            public string Email { get; set; } = null!;

        public DateTime BirthDate { get; set; }
        public List<AutorBookDtorel> Books { get; set; } = new List<AutorBookDtorel>();
    }
}