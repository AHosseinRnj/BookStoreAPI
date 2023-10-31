using Application.Commands.CreateBook;
using Application.Commands.CreateUser;
using Application.Commands.DeleteUser;
using Application.Commands.UpdateUser;
using Application.Query.GetUser;
using Application.Query.GetUsers;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
            _logger = LogManager.GetLogger(typeof(UsersController));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserCommand request)
        {
            try
            {
                _logger.Info("Received a request to add a User.");
                await _sender.Send(request);
                _logger.Info("User added successfully.");
                return Created("successfull", "Done");
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding a User: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                _logger.Info("Received a request to get all Users");
                var result = await _sender.Send(new GetUsersQuery());
                _logger.Info("Users retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting User: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                _logger.Info("Received a request to get a User by ID: " + id);
                var result = await _sender.Send(new GetUserQuery(id));
                _logger.Info("User retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting a User: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
        {
            try
            {
                _logger.Info("Received a request to update a User.");
                await _sender.Send(new UpdateUserCommand(id, request));
                _logger.Info("User updated successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating a User: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                _logger.Info("Received a request to delete a User by ID: " + id);
                await _sender.Send(new DeleteUserCommand(id));
                _logger.Info("User deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting a User: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}