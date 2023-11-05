using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, GetBookQueryResponse>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;

        public GetBookQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetBookQueryHandler));
        }

        public async Task<GetBookQueryResponse> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            GetBookQueryResponse result;

            try
            {
                _logger.Info("Received a request to get a book by ID: " + request.id);
                result = await _unitOfWork.BookRepository.GetBookByIdAsync(request.id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting a book: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Book retrieved successfully.");
            return result;
        }
    }
}