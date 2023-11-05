using Application.Query.GetAuthorBooks;
using Application.Query.GetBook;
using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.Author.GetAuthorBooks
{
    public class GetAuthorBooksHandler : IRequestHandler<GetAuthorBooksQuery, IEnumerable<GetBookQueryResponse>>
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorRepository _authorRepository;
        public GetAuthorBooksHandler(IUnitOfWork unitOfWork, IAuthorRepository authorRepository)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
            _logger = LogManager.GetLogger(typeof(GetAuthorBooksHandler));
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetAuthorBooksQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetBookQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get an Author's Books by ID: " + request.id);
                result = await _authorRepository.GetAuthorBooksAsync(request.id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting an Author's Books: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Author's Books retrieved successfully.");
            return result;
        }
    }
}