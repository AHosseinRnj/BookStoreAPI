using Application.Commands.CreatePublisher;
using Application.Commands.DeletePublisher;
using Application.Commands.UpdatePublisher;
using Application.Query.GetPublisher;
using Application.Query.GetPublisherBooks;
using Application.Query.GetPublishers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly ISender _sender;
        public PublishersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher(CreatePublisherCommand request)
        {
            await _sender.Send(request);
            return CreatedAtRoute("GetPublisherById", new { id = request }, request);
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
            var result = await _sender.Send(new GetPublishersQuery());

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetPublisherById")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            var result = await _sender.Send(new GetPublisherQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}/Books", Name = "GetPublisherBooks")]
        public async Task<IActionResult> GetPublisherBooks(int id)
        {
            var result = await _sender.Send(new GetPublisherBooksQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePublisher(UpdatePublisherCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePublisher(int id)
        {
            await _sender.Send(new DeletePublisherCommand(id));
            return NoContent();
        }
    }
}