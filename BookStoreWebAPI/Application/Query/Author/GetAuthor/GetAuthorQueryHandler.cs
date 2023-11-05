using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.Author.GetAuthor
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, GetAuthorQueryResponse>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public GetAuthorQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetAuthorQueryHandler));
        }

        public async Task<GetAuthorQueryResponse> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            GetAuthorQueryResponse result;

            try
            {
                _logger.Info("Received a request to get an Author by ID: " + request.id);
                result = await _unitOfWork.AuthorRepository.GetAuthorById(request.id);
            }
            catch (Exception ex)
            {
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