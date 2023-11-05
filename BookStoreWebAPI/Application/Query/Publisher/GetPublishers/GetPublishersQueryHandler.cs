using Application.Query.GetPublisher;
using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetPublishers
{
    public class GetPublishersQueryHandler : IRequestHandler<GetPublishersQuery, IEnumerable<GetPublisherQueryResponse>>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public GetPublishersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetPublishersQueryHandler));
        }

        public async Task<IEnumerable<GetPublisherQueryResponse>> Handle(GetPublishersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetPublisherQueryResponse> result;

            try
            {
                _logger.Info("Received a request to get Publishers");
                result = await _unitOfWork.PublisherRepository.GetPublishersAsync();
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting Publishers: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Publishers retrieved successfully.");
            return result;
        }
    }
}