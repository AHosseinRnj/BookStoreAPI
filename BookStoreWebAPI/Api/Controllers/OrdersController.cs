using Application.Command.CreateOrder;
using Application.Query.GetOrder;
using Application.Query.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;

        public OrdersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderCommand request)
        {
            await _sender.Send(request);
            return CreatedAtRoute("GetOrderById", new { id = request }, request);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _sender.Send(new GetOrdersQuery());

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _sender.Send(new GetOrderQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}