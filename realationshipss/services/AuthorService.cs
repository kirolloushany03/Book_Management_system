using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using realationshipss.data;
using realationshipss.DTOS.AuthorDtos.Requests;
using realationshipss.DTOS.AuthorDtos.Responses;
using realationshipss.Entities;

namespace realationshipss.services
{
    public class AuthorService : IAuthorService
    {
        private readonly Datacontext _context;
        private readonly IMapper _mapper;
        public AuthorService(Datacontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAuthors(int PageNumber = 1, int PageSize = 10)
        { 
            var Authors = await _context.TbAuthors
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(); // => so this one excue the query and asynchronously
                                //      retrives the result as list

            return _mapper.Map<IEnumerable<AuthorDto>>(Authors);
        }

        public async Task<AuthorWithID> GetAuthorById(int id)
        {
            var Author = await _context.TbAuthors.FindAsync(id);
            if (Author == null) throw new Exception("Author not found");

            return _mapper.Map<AuthorWithID>(Author);
        }

        public async Task<AuthorDto> CreateAuthor(CreateAuthorDto CreateAuthorDto)
        {
            var author = new Author
            {
                FirstName = CreateAuthorDto.FirstName,
                LastName = CreateAuthorDto.LastName,
                Email = CreateAuthorDto.Email,
                BirthDate = CreateAuthorDto.BirthDate
            };

            _context.TbAuthors.Add(author);
            await _context.SaveChangesAsync();

            return _mapper.Map<AuthorDto>(author);
        }


        public async Task<AuthorDto> UpdateAuthor(UpdateAuthorDto UpdateAuthorDto)
        {
            var author = await _context.TbAuthors.FindAsync(UpdateAuthorDto.Id);

            if (author == null)
            { 
                throw new Exception("Author not found"); 
            }

            if (UpdateAuthorDto.FirstName != null)
            { 
                author.FirstName = UpdateAuthorDto.FirstName;
            }
            
            if (UpdateAuthorDto.LastName != null)
            {
                author.LastName = UpdateAuthorDto.LastName;
            }
            if (UpdateAuthorDto.Email != null)
            {
                author.Email = UpdateAuthorDto.Email;
            }
            if (UpdateAuthorDto.BirthDate.HasValue)
            {
                author.BirthDate = UpdateAuthorDto.BirthDate.Value;
            }

            _context.TbAuthors.Update(author);
            await _context.SaveChangesAsync();

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var author = await _context.TbAuthors.FindAsync(id);
            if (author == null) return false;

            _context.TbAuthors.Remove(author);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
