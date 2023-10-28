using Application.Commands.CreateAuthor;
using Application.Commands.DeleteAuthor;
using Application.Commands.UpdateAuthor;
using Application.Query.Author.GetAuthor;
using Application.Query.GetAuthorBooks;
using Application.Query.GetAuthors;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly ISender _sender;
        public AuthorsController(ISender sender)
        {
            _sender = sender;
            _logger = LogManager.GetLogger(typeof(AuthorsController));
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(CreateAuthorCommand request)
        {
            try
            {
                _logger.Info("Received a request to add an Author.");
                await _sender.Send(request);
                _logger.Info("Author added successfully.");
                return Created("successfull", "Done");
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding an Author: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                _logger.Info("Received a request to get Authors");
                var result = await _sender.Send(new GetAuthorsQuery());
                _logger.Info("Authors retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting Authors: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}", Name = "GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {
                _logger.Info("Received a request to get an Author by ID: " + id);
                var result = await _sender.Send(new GetAuthorQuery(id));
                _logger.Info("Author retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting an Author: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}/Books", Name = "GetAuthorBooks")]
        public async Task<IActionResult> GetAuthorBooks(int id)
        {
            try
            {
                _logger.Info("Received a request to get an Author's Books by ID: " + id);
                var result = await _sender.Send(new GetAuthorBooksQuery(id));
                _logger.Info("Author's Books retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting an Author's Books: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(UpdateAuthorCommand request)
        {
            try
            {
                _logger.Info("Received a request to update an Author.");
                await _sender.Send(request);
                _logger.Info("Author updated successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating an Author: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            try
            {
                _logger.Info("Received a request to delete a Author by ID: " + id);
                await _sender.Send(new DeleteAuthorCommand(id));
                _logger.Info("Author deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting an Author: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}