using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using realationshipss.DTOS.BookAuthorDtos;
using realationshipss.services;

namespace realationshipss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorController : ControllerBase
    {
        private readonly IBookAuthorService _bookAuthorService;

        public BookAuthorController(IBookAuthorService bookAuthorService)
        {
            _bookAuthorService = bookAuthorService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBookAuthor(BookAuthorDto dto)
        {
            try
            {
                var cbookauthor = await _bookAuthorService.CreateBookAuthor(dto);
                return CreatedAtAction(nameof(CreateBookAuthor), new { bookId = cbookauthor.BookId, authorId = cbookauthor.AuthorId }, cbookauthor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookAuthor(BookAuthorDto dto)
        {
            try
            {
                var ubookauthor = await _bookAuthorService.UpdateBookAuthor(dto);
                return Ok(ubookauthor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
