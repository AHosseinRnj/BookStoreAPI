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
        public GetAuthorBooksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetAuthorBooksHandler));
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetAuthorBooksQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetBookQueryResponse> result;

            try
            {
                _logger.Info("Received a request to get an Author's Books by ID: " + request.id);
                result = await _unitOfWork.AuthorRepository.GetAuthorBooksAsync(request.id);
            }
            catch (Exception ex)
            {
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