using Application.Command.CreateOrder;
using Domain.Entities;
using log4net;

namespace Application.Services
{
    public class OrderWriteService : IOrderWriteService
    {
        private readonly ILog _logger;
        private readonly IEFUnitOfWork _unitOfWork;
        public OrderWriteService(IEFUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(OrderWriteService));
        }

        public async Task AddAsync(CreateOrderCommand request)
        {
            try
            {
                var order = new Order
                {
                    UserId = request.Order.UserId,
                    OrderDate = request.Order.OrderDate,

                    OrderItems = request.Order.Items.Select(item => new OrderItem
                    {
                        BookId = item.BookId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    }).ToList()
                };

                await _unitOfWork.OrderRepository.AddAsync(order);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding an Order: " + ex.Message, ex);
                throw;
            }

            _logger.Info("Order added successfully.");
        }
    }
}