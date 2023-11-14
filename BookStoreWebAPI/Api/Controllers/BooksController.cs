using Application.Commands.CreateBook;
using Application.Commands.DeleteBook;
using Application.Commands.UpdateBook;
using Application.Query.GetBook;
using Application.Query.GetBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ISender _sender;

        public BooksController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(CreateBookCommand request)
        {
            await _sender.Send(request);
            return CreatedAtRoute("GetBookById", new { id = request }, request);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var result = await _sender.Send(new GetBooksQuery());

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _sender.Send(new GetBookQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto bookDto)
        {
            await _sender.Send(new UpdateBookCommand(id, bookDto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            await _sender.Send(new DeleteBookCommand(id));
            return NoContent();
        }
    }
}