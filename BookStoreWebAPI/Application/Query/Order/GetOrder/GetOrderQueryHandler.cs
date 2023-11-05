using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, GetOrderQueryResponse>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public GetOrderQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetOrderQueryHandler));
        }

        public async Task<GetOrderQueryResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            GetOrderQueryResponse order;

            try
            {
                _logger.Info("Received a request to get an Order by ID: " + request.id);
                order = await _unitOfWork.OrderRepository.GetOrderByIdAsync(request.id);
            }
            catch (Exception ex)
            {
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