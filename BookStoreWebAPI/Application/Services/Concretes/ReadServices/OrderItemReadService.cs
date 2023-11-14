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
            var orderBooks = await _orderBookRepository.GetOrderItemAsync();

            var result = orderBooks.Select(oi => new GetOrderItemQueryResponse
            {
                Id = oi.Id,
                BookId = oi.BookId,
                Price = oi.Price,
                Quantity = oi.Quantity,
                OrderId = oi.OrderId
            });

            return result;
        }
    }
}