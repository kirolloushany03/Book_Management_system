using realationshipss.DTOS.BookDtos.Requests;
using realationshipss.DTOS.BookDtos.Responses;

namespace realationshipss.services
{
    public interface IBookService
    {
        Task<IEnumerable<BooksDto>> GetBooks(int PageNumber = 1, int PageSize = 10);
        Task<BookWithID> GetBookById(int id);
        Task<BooksDto> CreateBook(CreateBookDto CreateBookDto);
        Task<BooksDto> UpdateBook(UpdateBookDto updateBookDto);
        Task<bool> DeleteBook(int id);

    }
}
