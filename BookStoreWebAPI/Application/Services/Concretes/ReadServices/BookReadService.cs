using Application.Query.GetBook;
using Application.Repositories;
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
            var book = await _bookRepository.GetBookByIdAsync(id);

            var result = new GetBookQueryResponse
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                ISBN = book.ISBN,
                Quantity = book.Quantity,
            };

            return result;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetBooksAsync()
        {
            var listOfBooks = await _bookRepository.GetBooksAsync();

            var result = listOfBooks.Select(b => new GetBookQueryResponse
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                ISBN = b.ISBN,
                Quantity = b.Quantity,
            }).ToList();

            return result;
        }
    }
}