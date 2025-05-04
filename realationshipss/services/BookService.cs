using System.Drawing.Printing;
using AutoMapper;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.ValueContentAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using realationshipss.data;
using realationshipss.DTOS.BookDtos.Requests;
using realationshipss.DTOS.BookDtos.Responses;
using realationshipss.Entities;

namespace realationshipss.services
{
    public class BookService : IBookService
    {
        private readonly Datacontext _context;
        private readonly IMapper _mapper;

        public BookService(Datacontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //getting all the books method
        public async Task<IEnumerable<BooksDto>> GetBooks(int PageNumber = 1, int PageSize = 10)
        {
            var books = await _context.TbBOOK
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BooksDto>>(books);
            
        }

        //getting book by id
        public async Task<BookWithID> GetBookById(int id)
        {
            var book = await _context.TbBOOK.FindAsync(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            return _mapper.Map<BookWithID>(book);
        }

        // creating new book
        public async Task<BooksDto> CreateBook(CreateBookDto CreateBookDto)
        {
            var book = new Book
            {
                Name = CreateBookDto.Name,
                Avaliable = CreateBookDto.Avaliable,
                ReleaseDate = CreateBookDto.ReleaseDate,
                Price = CreateBookDto.Price
            };

            _context.TbBOOK.Add(book);
            await _context.SaveChangesAsync();

            return _mapper.Map<BooksDto>(book);
        }

        //updating book
        public async Task<BooksDto> UpdateBook(UpdateBookDto updateBookDto)
        {
            var book = await _context.TbBOOK.FindAsync(updateBookDto.Id);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            // Manually check each property and update if it's not null
            if (updateBookDto.Name != null)
            {
                book.Name = updateBookDto.Name;
            }
            if (updateBookDto.ReleaseDate.HasValue)
            {
                book.ReleaseDate = updateBookDto.ReleaseDate.Value;
            }
            if (updateBookDto.Avaliable.HasValue)
            {
                book.Avaliable = updateBookDto.Avaliable.Value;
            }
            if (updateBookDto.Price.HasValue)
            {
                book.Price = updateBookDto.Price.Value;
            }

            _context.TbBOOK.Update(book);
            await _context.SaveChangesAsync();

            return _mapper.Map<BooksDto>(book);
        }

        //delete by book id
        public async Task<bool> DeleteBook(int id)
        {
            var book = await _context.TbBOOK.FindAsync(id);
            if (book == null) return false;

            _context.TbBOOK.Remove(book);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}