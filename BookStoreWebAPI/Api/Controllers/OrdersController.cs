using Application.Command.CreateOrder;
using Application.Query.GetOrder;
using Application.Query.GetOrders;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly ISender _sender;

        public OrdersController(ISender sender)
        {
            _sender = sender;
            _logger = LogManager.GetLogger(typeof(OrdersController));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderCommand request)
        {
            try
            {
                _logger.Info("Received a request to add an Order.");
                await _sender.Send(request);
                _logger.Info("Order added successfully.");
                return Created("successfull", "Done");
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding an Order: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                _logger.Info("Received a request to get all Orders");
                var result = await _sender.Send(new GetOrdersQuery());
                _logger.Info("Orders retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting Orders: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                _logger.Info("Received a request to get an Order by ID: " + id);
                var result = await _sender.Send(new GetOrderQuery(id));
                _logger.Info("Order retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting an Order: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}