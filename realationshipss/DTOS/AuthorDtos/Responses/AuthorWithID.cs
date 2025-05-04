using System.ComponentModel.DataAnnotations;
using realationshipss.DTOS.BookAuthorDtos;
using realationshipss.DTOS.BookDtos.Responses;

namespace realationshipss.DTOS.AuthorDtos.Responses
{
    public class AuthorWithID
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
