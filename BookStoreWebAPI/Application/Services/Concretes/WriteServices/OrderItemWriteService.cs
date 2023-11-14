using Application.Commands.CreateOrderBook;
using Application.Repositories;
using Domain.Entities;
using Infrastructure.Services;
using log4net;

namespace Application.Services
{
    public class OrderItemWriteService : IOrderItemWriteService
    {
        private readonly ILog _logger;
        private IDapperUnitOfWork _unitOfWork;
        private readonly IOrderItemWriteRepository _orderBookRepository;
        public OrderItemWriteService(IDapperUnitOfWork unitOfWork, IOrderItemWriteRepository orderBookRepository)
        {
            _unitOfWork = unitOfWork;
            _orderBookRepository = orderBookRepository;
            _logger = LogManager.GetLogger(typeof(OrderItemWriteService));
        }

        public async Task AddAsync(CreateOrderItemCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add an OrderBook.");

                var orderbook = new OrderItem
                {
                    OrderId = request.OrderId,
                    BookId = request.BookId,
                    Quantity = request.Quantity,
                    Price = request.Price
                };

                await _orderBookRepository.AddAsync(orderbook);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error adding an OrderBook: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("OrderBook added successfully.");
        }
    }
}