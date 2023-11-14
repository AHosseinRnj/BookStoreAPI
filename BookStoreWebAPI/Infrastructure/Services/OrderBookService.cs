using Application;
using Application.Commands.CreateOrderBook;
using Application.Query.GetOrderBook;
using Application.Repositpries;
using Application.Services;
using Domain.Entities;
using log4net;

namespace Infrastructure.Services
{
    public class OrderBookService : IOrderBookService
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IOrderBookRepository _orderBookRepository;
        public OrderBookService(IUnitOfWork unitOfWork, IOrderBookRepository orderBookRepository)
        {
            _unitOfWork = unitOfWork;
            _orderBookRepository = orderBookRepository;
            _logger = LogManager.GetLogger(typeof(OrderBookService));
        }

        public async Task AddAsync(CreateOrderItemCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add an OrderBook.");

                var orderbook = new OrderItem
                {
                    OrderId = request.orderId,
                    BookId = request.bookId,
                    Quantity = request.quantity,
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

        public async Task<IEnumerable<GetOrderBookQueryResponse>> GetOrderBooksAsync()
        {
            IEnumerable<GetOrderBookQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get all OrderBooks");

                var orderBooks = await _orderBookRepository.GetOrderBooksAsync();

                result = orderBooks.Select(ob => new GetOrderBookQueryResponse
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