using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetPublisher
{
    public class GetPublisherQueryHandler : IRequestHandler<GetPublisherQuery, GetPublisherQueryResponse>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public GetPublisherQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetPublisherQueryHandler));
        }

        public async Task<GetPublisherQueryResponse> Handle(GetPublisherQuery request, CancellationToken cancellationToken)
        {
            GetPublisherQueryResponse result;

            try
            {
                _logger.Info("Received a request to get a Publisher by ID: " + request.id);
                result = await _unitOfWork.PublisherRepository.GetPublisherByIdAsync(request.id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting a Publisher: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Publisher retrieved successfully.");
            return result;
        }
    }
}