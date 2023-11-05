using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, GetOrderQueryResponse>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        public GetOrderQueryHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _logger = LogManager.GetLogger(typeof(GetOrderQueryHandler));
        }

        public async Task<GetOrderQueryResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            GetOrderQueryResponse order;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get an Order by ID: " + request.id);
                order = await _orderRepository.GetOrderByIdAsync(request.id);
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
            return order;
        }
    }
}