using System.Drawing.Printing;
using System.Security.Cryptography.Pkcs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using realationshipss.data;
using realationshipss.DTOS.BookDtos.Requests;
using realationshipss.DTOS.BookDtos.Responses;
using realationshipss.services;

namespace realationshipss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService) {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var books = await _bookService.GetBooks(pageNumber, pageSize);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookById(id);
                return Ok(book);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookDto CreateBookDto)
        { 
            var book = await _bookService.CreateBook(CreateBookDto);
            return CreatedAtAction(nameof(GetBookById), new {id = book.Id}, book);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(UpdateBookDto updateBookDto)
        {
            try
            {
                var updatedbook = await _bookService.UpdateBook(updateBookDto);
                return Ok(updatedbook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        { 
            var delete = await _bookService.DeleteBook(id);
            if (!delete)
            {
                return NotFound(new { message = "Book not found" });
            }
            return NoContent();
        }
    }
}
