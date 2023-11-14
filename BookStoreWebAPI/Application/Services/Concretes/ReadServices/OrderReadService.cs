using Application.Query.GetOrder;
using Application.Repositories;
using Infrastructure.Services;
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
            GetOrderQueryResponse result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get an Order by ID: " + id);

                var order = await _orderRepository.GetOrderByIdAsync(id);

                result = new GetOrderQueryResponse
                {
                    OrderDate = order.OrderDate,
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting an Order: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Order retrieved successfully.");
            return result;
        }

        public async Task<IEnumerable<GetOrderQueryResponse>> GetOrdersAsync()
        {
            List<GetOrderQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get all Orders");

                var listOfOrders = await _orderRepository.GetOrdersAsync();

                result = listOfOrders.Select(o => new GetOrderQueryResponse
                {
                    OrderDate = o.OrderDate
                }).ToList();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting Orders: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Orders retrieved successfully.");
            return result;
        }
    }
}