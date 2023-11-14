using Application.Commands.CreateOrderBook;
using Application.Query.GetOrderBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly ISender _sender;

        public OrderItemsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderBooks(CreateOrderItemCommand request)
        {
            await _sender.Send(request);
            return Created("Successfull", request);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderBooks()
        {
            var result = await _sender.Send(new GetOrderItemQuery());
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}