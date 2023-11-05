using Application.Query.GetOrder;
using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<GetOrderQueryResponse>>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        public GetOrdersQueryHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _logger = LogManager.GetLogger(typeof(GetOrdersQueryHandler));
        }

        public async Task<IEnumerable<GetOrderQueryResponse>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetOrderQueryResponse> listOfOrders;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get all Orders");
                listOfOrders = await _orderRepository.GetOrdersAsync();
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
            return listOfOrders;
        }
    }
}