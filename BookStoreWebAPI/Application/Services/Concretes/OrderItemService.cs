using Application;
using Application.Commands.CreateOrderBook;
using Application.Query.GetOrderBook;
using Application.Repositpries;
using Application.Services;
using Domain.Entities;
using log4net;

namespace Infrastructure.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ILog _logger;
        private IDapperUnitOfWork _unitOfWork;
        private readonly IOrderItemRepository _orderBookRepository;
        public OrderItemService(IDapperUnitOfWork unitOfWork, IOrderItemRepository orderBookRepository)
        {
            _unitOfWork = unitOfWork;
            _orderBookRepository = orderBookRepository;
            _logger = LogManager.GetLogger(typeof(OrderItemService));
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

        public async Task<IEnumerable<GetOrderItemQueryResponse>> GetOrderBooksAsync()
        {
            IEnumerable<GetOrderItemQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get all OrderBooks");

                var orderBooks = await _orderBookRepository.GetOrderItemAsync();

                result = orderBooks.Select(ob => new GetOrderItemQueryResponse
                {
                    BookId = ob.BookId,
                    Price = ob.Price,
                    Quantity = ob.Quantity,
                });
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting OrderBooks: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("OrderBooks retrieved successfully.");
            return result;
        }
    }
}