using Application.Commands.CreateBook;
using Application.Commands.DeleteBook;
using Application.Commands.UpdateBook;
using Application.Query.GetBook;
using log4net;
using log4net.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly ISender _sender;

        public BookController(ISender sender)
        {
            _sender = sender;
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            _logger = LogManager.GetLogger(typeof(BookController));
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