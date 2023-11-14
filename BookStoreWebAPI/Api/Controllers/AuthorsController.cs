using Application.Commands.CreateAuthor;
using Application.Commands.DeleteAuthor;
using Application.Commands.UpdateAuthor;
using Application.Query.Author.GetAuthor;
using Application.Query.GetAuthorBooks;
using Application.Query.GetAuthors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ISender _sender;
        public AuthorsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(CreateAuthorCommand request)
        {
            await _sender.Send(request);
            return CreatedAtRoute("GetAuthorById", new { id = request}, request);
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var result = await _sender.Send(new GetAuthorsQuery());

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var result = await _sender.Send(new GetAuthorQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}/Books", Name = "GetAuthorBooks")]
        public async Task<IActionResult> GetAuthorBooks(int id)
        {
            var result = await _sender.Send(new GetAuthorBooksQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(UpdateAuthorCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            await _sender.Send(new DeleteAuthorCommand(id));
            return NoContent();
        }
    }
}