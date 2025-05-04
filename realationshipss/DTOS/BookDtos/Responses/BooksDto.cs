using System.ComponentModel.DataAnnotations;
using realationshipss.DTOS.BookAuthorDtos;

namespace realationshipss.DTOS.BookDtos.Responses
{
    public class BooksDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
            public string Name { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }
        public int Avaliable { get; set; }
        public int Price { get; set; }
        public List<BookAuthordtorel> Authors { get; set; } = new List<BookAuthordtorel>();
    }
}
