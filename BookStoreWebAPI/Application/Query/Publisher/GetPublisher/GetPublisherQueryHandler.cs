using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetPublisher
{
    public class GetPublisherQueryHandler : IRequestHandler<GetPublisherQuery, GetPublisherQueryResponse>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IPublisherRepository _publisherRepository;
        public GetPublisherQueryHandler(IUnitOfWork unitOfWork, IPublisherRepository publisherRepository)
        {
            _unitOfWork = unitOfWork;
            _publisherRepository = publisherRepository;
            _logger = LogManager.GetLogger(typeof(GetPublisherQueryHandler));
        }

        public async Task<GetPublisherQueryResponse> Handle(GetPublisherQuery request, CancellationToken cancellationToken)
        {
            GetPublisherQueryResponse result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get a Publisher by ID: " + request.id);
                result = await _publisherRepository.GetPublisherByIdAsync(request.id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
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