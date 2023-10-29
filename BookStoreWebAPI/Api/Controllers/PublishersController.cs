using Application.Commands.CreatePublisher;
using Application.Commands.DeletePublisher;
using Application.Commands.UpdatePublisher;
using Application.Query.GetPublisher;
using Application.Query.GetPublisherBooks;
using Application.Query.GetPublishers;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly ISender _sender;
        public PublishersController(ISender sender)
        {
            _sender = sender;
            _logger = LogManager.GetLogger(typeof(PublishersController));
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher(CreatePublisherCommand request)
        {
            try
            {
                _logger.Info("Received a request to add a Publisher.");
                await _sender.Send(request);
                _logger.Info("Publisher added successfully.");
                return Created("successfull", "Done");
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding a Publisher: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
            try
            {
                _logger.Info("Received a request to get Publishers");
                var result = await _sender.Send(new GetPublishersQuery());
                _logger.Info("Publishers retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting Publishers: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}", Name = "GetPublisherById")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            try
            {
                _logger.Info("Received a request to get a Publisher by ID: " + id);
                var result = await _sender.Send(new GetPublisherQuery(id));
                _logger.Info("Publisher retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting a Publisher: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}/Books", Name = "GetPublisherBooks")]
        public async Task<IActionResult> GetPublisherBooks(int id)
        {
            try
            {
                _logger.Info("Received a request to get a Publisher's Books by ID: " + id);
                var result = await _sender.Send(new GetPublisherBooksQuery(id));
                _logger.Info("Publisher's Books retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting an Publisher's Books: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePublisher(UpdatePublisherCommand request)
        {
            try
            {
                _logger.Info("Received a request to update a Publisher.");
                await _sender.Send(request);
                _logger.Info("Publisher updated successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating a Publisher: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePublisher(int id)
        {
            try
            {
                _logger.Info("Received a request to delete a Publisher by ID: " + id);
                await _sender.Send(new DeletePublisherCommand(id));
                _logger.Info("Publisher deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting a Publisher: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}