using System.ComponentModel.DataAnnotations;

namespace realationshipss.DTOS.BookDtos.Requests
{
    public class UpdateBookDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
            public string? Name { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public int? Avaliable { get; set; }
        public int? Price { get; set; }
    }
}
