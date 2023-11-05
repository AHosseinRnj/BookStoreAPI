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
        private readonly IPublisherRepository _publisherRepository;
        public GetPublishersQueryHandler(IUnitOfWork unitOfWork, IPublisherRepository publisherRepository)
        {
            _unitOfWork = unitOfWork;
            _publisherRepository = publisherRepository;
            _logger = LogManager.GetLogger(typeof(GetPublishersQueryHandler));
        }

        public async Task<IEnumerable<GetPublisherQueryResponse>> Handle(GetPublishersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetPublisherQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get Publishers");
                result = await _publisherRepository.GetPublishersAsync();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
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