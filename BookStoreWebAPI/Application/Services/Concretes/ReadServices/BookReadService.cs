using Application.Query.GetBook;
using Application.Repositories;
using Infrastructure.Services;
using log4net;

namespace Application.Services
{
    public class BookReadService : IBookReadService
    {
        private readonly ILog _logger;
        private readonly IDapperUnitOfWork _unitOfWork;
        private readonly IBookReadRepository _bookRepository;
        public BookReadService(IDapperUnitOfWork unitOfWork, IBookReadRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _logger = LogManager.GetLogger(typeof(BookReadService));
        }

        public async Task<GetBookQueryResponse> GetBookByIdAsync(int id)
        {
            GetBookQueryResponse result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get a book by ID: " + id);

                var book = await _bookRepository.GetBookByIdAsync(id);

                result = new GetBookQueryResponse
                {
                    Title = book.Title,
                    Price = book.Price,
                    ISBN = book.ISBN
                };
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

        public async Task<IEnumerable<GetBookQueryResponse>> GetBooksAsync()
        {
            List<GetBookQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get all books");

                var listOfBooks = await _bookRepository.GetBooksAsync();

                result = listOfBooks.Select(b => new GetBookQueryResponse
                {
                    Title = b.Title,
                    ISBN = b.ISBN,
                    Price = b.Price
                }).ToList();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting Books: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Books retrieved successfully.");
            return result;
        }
    }
}