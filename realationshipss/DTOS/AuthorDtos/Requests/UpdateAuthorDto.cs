using System.ComponentModel.DataAnnotations;

namespace realationshipss.DTOS.AuthorDtos.Requests
{
    public class UpdateAuthorDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }
 
        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
