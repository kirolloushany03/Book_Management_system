using realationshipss.DTOS.AuthorDtos.Requests;
using realationshipss.DTOS.AuthorDtos.Responses;

namespace realationshipss.services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAuthors(int PageNumber = 1, int PageSize = 10);
        Task<AuthorWithID> GetAuthorById(int id);
        Task<AuthorDto> CreateAuthor(CreateAuthorDto CreateAuthorDto);
        Task<AuthorDto> UpdateAuthor(UpdateAuthorDto UpdateAuthorDto);
        Task<bool> DeleteAuthor(int id);

    }
}
