using Application.Query.GetOrderBook;
using Application.Repositories;
using log4net;

namespace Application.Services
{
    public class OrderItemReadService : IOrderItemReadService
    {
        private readonly ILog _logger;
        private IDapperUnitOfWork _unitOfWork;
        private readonly IOrderItemReadRepository _orderBookRepository;
        public OrderItemReadService(IDapperUnitOfWork unitOfWork, IOrderItemReadRepository orderBookRepository)
        {
            _unitOfWork = unitOfWork;
            _orderBookRepository = orderBookRepository;
            _logger = LogManager.GetLogger(typeof(OrderItemReadService));
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