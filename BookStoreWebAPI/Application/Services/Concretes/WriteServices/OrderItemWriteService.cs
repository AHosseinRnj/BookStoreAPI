using Application.Commands.CreateOrderBook;
using Domain.Entities;
using log4net;

namespace Application.Services
{
    public class OrderItemWriteService : IOrderItemWriteService
    {
        private readonly ILog _logger;
        private IEFUnitOfWork _unitOfWork;
        public OrderItemWriteService(IEFUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(OrderItemWriteService));
        }

        public async Task AddAsync(CreateOrderItemCommand request)
        {
            try
            {
                var orderItem = new OrderItem
                {
                    OrderId = request.OrderId,
                    BookId = request.BookId,
                    Quantity = request.Quantity,
                    Price = request.Price
                };

                await _unitOfWork.OrderItemRepository.AddAsync(orderItem);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding an OrderBook: " + ex.Message, ex);
                throw;
            }
        }
    }
}