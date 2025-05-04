using System.ComponentModel.DataAnnotations;

namespace realationshipss.Entities
{
    public class Author: BaseEntity
    {
        [Required]
        [Key]
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

        //so it better to be like this or to be with List or to be = null! or to be like this ??
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

}
}
