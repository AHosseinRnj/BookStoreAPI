using Application.Commands.CreateOrderBook;
using Application.Query.GetOrderBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderBooksController : ControllerBase
    {
        private readonly ISender _sender;

        public OrderBooksController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderBooks(CreateOrderBookCommand request)
        {
            await _sender.Send(request);
            return Created("Successfull", request);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderBooks()
        {
            var result = await _sender.Send(new GetOrderBooksQuery());
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}