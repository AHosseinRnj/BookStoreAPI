using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, GetBookQueryResponse>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        public GetBookQueryHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _logger = LogManager.GetLogger(typeof(GetBookQueryHandler));
        }

        public async Task<GetBookQueryResponse> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            GetBookQueryResponse result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get a book by ID: " + request.id);
                result = await _bookRepository.GetBookByIdAsync(request.id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
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