using Application.Query.GetBook;
using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetCategoryBooks
{
    public class GetCategoryBooksHandler : IRequestHandler<GetCategoryBooksQuery, IEnumerable<GetBookQueryResponse>>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public GetCategoryBooksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetCategoryBooksHandler));
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetCategoryBooksQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetBookQueryResponse> listOfBooks;

            try
            {
                _logger.Info("Received a request to get a Category's Books by ID: " + request.id);
                listOfBooks = await _unitOfWork.CategoryRepository.GetCategoryBooksAsync(request.id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error getting an Category's Books: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Category's Books retrieved successfully.");
            return listOfBooks;
        }
    }
}