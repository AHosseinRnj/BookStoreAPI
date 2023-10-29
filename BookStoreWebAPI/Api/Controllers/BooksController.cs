using Application.Commands.CreateBook;
using Application.Commands.DeleteBook;
using Application.Commands.UpdateBook;
using Application.Query.GetBook;
using Application.Query.GetBooks;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly ISender _sender;

        public BooksController(ISender sender)
        {
            _sender = sender;
            _logger = LogManager.GetLogger(typeof(BooksController));
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(CreateBookCommand request)
        {
            try
            {
                _logger.Info("Received a request to add a book.");
                await _sender.Send(request);
                _logger.Info("Book added successfully.");
                return Created("successfull", "Done");
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding a book: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                _logger.Info("Received a request to get all books");
                var result = await _sender.Send(new GetBooksQuery());
                _logger.Info("Books retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting Books: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                _logger.Info("Received a request to get a book by ID: " + id);
                var result = await _sender.Send(new GetBookQuery(id));
                _logger.Info("Book retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting a book: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(UpdateBookCommand request)
        {
            try
            {
                _logger.Info("Received a request to update a book.");
                await _sender.Send(request);
                _logger.Info("Book updated successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating a book: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                _logger.Info("Received a request to delete a book by ID: " + id);
                await _sender.Send(new DeleteBookCommand(id));
                _logger.Info("Book deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting a book: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}