using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetUserOrders
{
    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, IEnumerable<GetUserOrdersQueryResponse>>
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetUserOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetUserOrdersQueryHandler));
        }

        public async Task<IEnumerable<GetUserOrdersQueryResponse>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetUserOrdersQueryResponse> listOfOrders;

            try
            {
                _logger.Info("Received a request to get an User's Orders by ID: " + request.id);
                listOfOrders = await _unitOfWork.UserRepository.GetUserOrdersById(request.id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting an User's Orders: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("User's Orders retrieved successfully.");
            return listOfOrders;
        }
    }
}