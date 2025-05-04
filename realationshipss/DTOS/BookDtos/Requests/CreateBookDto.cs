using System.ComponentModel.DataAnnotations;

namespace realationshipss.DTOS.BookDtos.Requests
{
    public class CreateBookDto
    {
        [MaxLength(50)]
        [Required]
            public string Name { get; set; } = null!;
        [Required]
            public DateTime ReleaseDate { get; set; }

        public int Avaliable { get; set; }
        public int Price { get; set; }

    }
}
