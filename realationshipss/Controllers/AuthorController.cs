using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using realationshipss.data;
using realationshipss.DTOS.AuthorDtos.Requests;
using realationshipss.Entities;
using realationshipss.services;

namespace realationshipss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService  _authorservice;

        public AuthorController(IAuthorService authorService)
        { 
            _authorservice = authorService;
        }

        //[Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetGetAuthors(int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var author = await _authorservice.GetAuthors(PageNumber, PageSize);
                return Ok(author);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {
                var author = await _authorservice.GetAuthorById(id);
                return Ok(author);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        //[Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateAuthorDto CreateAuthorDto)
        { 
            var author  = await _authorservice.CreateAuthor(CreateAuthorDto);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorDto UpdateAuthorDto)
        {
            try
            {
                var author = await _authorservice.UpdateAuthor(UpdateAuthorDto);
                return Ok(author);
            }
            catch (Exception ex)
            { 
                return StatusCode(500 , new { message = ex.Message});
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleteauthor = await _authorservice.DeleteAuthor(id);
            if (!deleteauthor)
            { 
                return NotFound(new { message  = "Author not found"});
            }
            return NoContent();
        }
    }
}
