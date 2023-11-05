using Application.Query.Author.GetAuthor;
using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetAuthors
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<GetAuthorQueryResponse>>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IAuthorRepository _authorRepository;
        public GetAuthorsQueryHandler(IUnitOfWork unitOfWork, IAuthorRepository authorRepository)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
            _logger = LogManager.GetLogger(typeof(GetAuthorsQueryHandler));
        }

        public async Task<IEnumerable<GetAuthorQueryResponse>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetAuthorQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get Authors");
                result = await _authorRepository.GetAuthorsAsync();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting Authors: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Authors retrieved successfully.");
            return result;
        }
    }
}