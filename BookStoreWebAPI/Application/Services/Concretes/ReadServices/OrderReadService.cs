using Application.Query.GetOrder;
using Application.Repositories;
using log4net;

namespace Application.Services
{
    public class OrderReadService : IOrderReadService
    {
        private readonly ILog _logger;
        private readonly IDapperUnitOfWork _unitOfWork;
        private readonly IOrderReadRepository _orderRepository;
        public OrderReadService(IDapperUnitOfWork unitOfWork, IOrderReadRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _logger = LogManager.GetLogger(typeof(OrderReadService));
        }

        public async Task<GetOrderQueryResponse> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            var result = new GetOrderQueryResponse
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
            };

            return result;
        }

        public async Task<IEnumerable<GetOrderQueryResponse>> GetOrdersAsync()
        {
            var listOfOrders = await _orderRepository.GetOrdersAsync();

            var result = listOfOrders.Select(o => new GetOrderQueryResponse
            {
                Id = o.Id,
                UserId = o.UserId,
                OrderDate = o.OrderDate
            }).ToList();

            return result;
        }
    }
}