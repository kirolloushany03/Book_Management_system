using realationshipss.DTOS.BookAuthorDtos;

namespace realationshipss.services
{
    public interface IBookAuthorService
    {
        Task<BookAuthorDto> CreateBookAuthor(BookAuthorDto dto);
        Task<BookAuthorDto> UpdateBookAuthor(BookAuthorDto dto);
    }
}
