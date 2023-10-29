using Application.Commands.CreateCategory;
using Application.Commands.DeleteCategory;
using Application.Commands.UpdateCategory;
using Application.Query.GetCategories;
using Application.Query.GetCategory;
using Application.Query.GetCategoryBooks;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorisController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly ISender _sender;
        public CategorisController(ISender sender)
        {
            _sender = sender;
            _logger = LogManager.GetLogger(typeof(PublishersController));
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryCommand request)
        {
            try
            {
                _logger.Info("Received a request to add a Category.");
                await _sender.Send(request);
                _logger.Info("Category added successfully.");
                return Created("successfull", "Done");
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding a Category: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                _logger.Info("Received a request to get Categories");
                var result = await _sender.Send(new GetCategoriesQuery());
                _logger.Info("Categories retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting Categories: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                _logger.Info("Received a request to get a Category by ID: " + id);
                var result = await _sender.Send(new GetCategoryQuery(id));
                _logger.Info("Category retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting a Category: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}/Books", Name = "GetCategoryBooks")]
        public async Task<IActionResult> GetCategoryBooks(int id)
        {
            try
            {
                _logger.Info("Received a request to get a Category's Books by ID: " + id);
                var result = await _sender.Send(new GetCategoryBooksQuery(id));
                _logger.Info("Category's Books retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting an Category's Books: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand request)
        {
            try
            {
                _logger.Info("Received a request to update a Category.");
                await _sender.Send(request);
                _logger.Info("Category updated successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating a Category: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                _logger.Info("Received a request to delete a Category by ID: " + id);
                await _sender.Send(new DeleteCategoryCommand(id));
                _logger.Info("Category deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting a Category: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}