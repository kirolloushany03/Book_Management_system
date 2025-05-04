using System.ComponentModel.DataAnnotations;

namespace realationshipss.Entities
{
    public class Book: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime ReleaseDate { get; set; }

        public int Avaliable { get; set; }
        public int Price { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
