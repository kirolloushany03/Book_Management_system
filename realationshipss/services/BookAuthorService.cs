using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using realationshipss.data;
using realationshipss.DTOS.BookAuthorDtos;
using realationshipss.Entities;
using realationshipss.Migrations;

namespace realationshipss.services
{
    public class BookAuthorService : IBookAuthorService
    {
        private readonly Datacontext _context;
        private readonly IMapper _mapper;

        public BookAuthorService(Datacontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookAuthorDto> CreateBookAuthor(BookAuthorDto dto)
        {
            
            var book = await _context.TbBOOK.FindAsync(dto.BookId);
            if (book == null) throw new Exception("Book not found");

            
            var author = await _context.TbAuthors.FindAsync(dto.AuthorId);
            if (author == null) throw new Exception("Author not found");

            var bookauthor = new BookAuthor
            {
                BookId = dto.BookId,
                AuthorId = dto.AuthorId,
                ContributionPercentage = dto.CPercent
            };

            _context.TbBookAuthor.Add(bookauthor);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookAuthorDto>(bookauthor);
        }

        public async Task<BookAuthorDto> UpdateBookAuthor(BookAuthorDto dto)
        { 
             var bookauthor = await _context.TbBookAuthor
                .FirstOrDefaultAsync(ba => ba.BookId == dto.BookId && ba.AuthorId == dto.AuthorId);

            if (bookauthor == null) throw new Exception("Bookauthor relatship not found");

            if (dto.CPercent > 0)
            {
                bookauthor.ContributionPercentage = dto.CPercent;
            }

            _context.TbBookAuthor.Update(bookauthor);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookAuthorDto>(bookauthor);
        }
    }
}
