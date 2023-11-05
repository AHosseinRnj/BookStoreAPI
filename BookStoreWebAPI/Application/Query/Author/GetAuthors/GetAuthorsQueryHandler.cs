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
        public GetAuthorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetAuthorsQueryHandler));
        }

        public async Task<IEnumerable<GetAuthorQueryResponse>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetAuthorQueryResponse> result;

            try
            {
                _logger.Info("Received a request to get Authors");
                result = await _unitOfWork.AuthorRepository.GetAuthorsAsync();
            }
            catch (Exception ex)
            {
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