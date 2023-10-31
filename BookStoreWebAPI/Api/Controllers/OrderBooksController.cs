using Application.Commands.CreateOrderBook;
using Application.Query.GetOrderBooks;
using Application.Query.GetOrders;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderBooksController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly ISender _sender;

        public OrderBooksController(ISender sender)
        {
            _sender = sender;
            _logger = LogManager.GetLogger(typeof(OrderBooksController));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderBooks(CreateOrderBookCommand request)
        {
            try
            {
                _logger.Info("Received a request to add an OrderBook.");
                await _sender.Send(request);
                _logger.Info("OrderBook added successfully.");
                return Created("successfull", "Done");
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding an OrderBook: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderBooks()
        {
            try
            {
                _logger.Info("Received a request to get all OrderBooks");
                var result = await _sender.Send(new GetOrderBooksQuery());
                _logger.Info("OrderBooks retrieved successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting OrderBooks: " + ex.Message, ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}