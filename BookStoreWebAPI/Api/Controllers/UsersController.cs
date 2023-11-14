using Application.Commands.CreateUser;
using Application.Commands.DeleteUser;
using Application.Commands.UpdateUser;
using Application.Query.GetUser;
using Application.Query.GetUserOrders;
using Application.Query.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserCommand request)
        {
            await _sender.Send(request);
            return CreatedAtRoute("GetUserById", new { id = request }, request);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _sender.Send(new GetUsersQuery());

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _sender.Send(new GetUserQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}/Orders", Name = "GetUserOrders")]
        public async Task<IActionResult> GetUserOrders(int id)
        {
            var result = await _sender.Send(new GetUserOrderItemQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
        {
            await _sender.Send(new UpdateUserCommand(id, request));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _sender.Send(new DeleteUserCommand(id));
            return NoContent();
        }
    }
}