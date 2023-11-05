using Application.Query.GetBook;
using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetPublisherBooks
{
    public class GetPublisherBooksQueryHandler : IRequestHandler<GetPublisherBooksQuery, IEnumerable<GetBookQueryResponse>>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public GetPublisherBooksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetPublisherBooksQueryHandler));
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetPublisherBooksQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetBookQueryResponse> result;

            try
            {
                _logger.Info("Received a request to get a Publisher's Books by ID: " + request.id);
                result = await _unitOfWork.PublisherRepository.GetPublisherBooksAsync(request.id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting an Publisher's Books: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Publisher's Books retrieved successfully.");
            return result;
        }
    }
}