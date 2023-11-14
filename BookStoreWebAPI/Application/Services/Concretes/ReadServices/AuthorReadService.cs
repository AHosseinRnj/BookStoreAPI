using Application.Query.Author.GetAuthor;
using Application.Query.GetBook;
using Application.Repositories;
using log4net;

namespace Application.Services
{
    public class AuthorReadService : IAuthorReadService
    {
        private readonly ILog _logger;
        private readonly IDapperUnitOfWork _unitOfWork;
        private readonly IAuthorReadRepository _authorRepository;
        public AuthorReadService(IDapperUnitOfWork unitOfWork, IAuthorReadRepository authorRepository)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
            _logger = LogManager.GetLogger(typeof(AuthorReadService));
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetAuthorBooksAsync(int id)
        {
            var listOfAuthorBooks = await _authorRepository.GetAuthorBooksAsync(id);

            var result = listOfAuthorBooks.Select(b => new GetBookQueryResponse
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                Price = b.Price,
                Quantity = b.Quantity,
            }).ToList();

            return result;
        }

        public async Task<GetAuthorQueryResponse> GetAuthorById(int id)
        {
            var author = await _authorRepository.GetAuthorById(id);

            var result = new GetAuthorQueryResponse
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Biography = author.Biography
            };

            return result;
        }

        public async Task<IEnumerable<GetAuthorQueryResponse>> GetAuthorsAsync()
        {
            var listOfAuthors = await _authorRepository.GetAuthorsAsync();

            var result = listOfAuthors.Select(a => new GetAuthorQueryResponse
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Biography = a.Biography
            }).ToList();

            return result;
        }
    }
}