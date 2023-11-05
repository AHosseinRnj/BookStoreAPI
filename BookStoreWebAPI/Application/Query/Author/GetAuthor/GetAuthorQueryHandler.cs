using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.Author.GetAuthor
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, GetAuthorQueryResponse>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IAuthorRepository _authorRepository;
        public GetAuthorQueryHandler(IUnitOfWork unitOfWork, IAuthorRepository authorRepository)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
            _logger = LogManager.GetLogger(typeof(GetAuthorQueryHandler));
        }

        public async Task<GetAuthorQueryResponse> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            GetAuthorQueryResponse result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get an Author by ID: " + request.id);
                result = await _authorRepository.GetAuthorById(request.id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting an Author: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Author retrieved successfully.");
            return result;
        }
    }
}