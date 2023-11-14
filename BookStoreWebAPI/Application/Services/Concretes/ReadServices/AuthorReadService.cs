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
            List<GetBookQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get an Author's Books by ID: " + id);

                var listOfAuthorBooks = await _authorRepository.GetAuthorBooksAsync(id);

                result = listOfAuthorBooks.Select(b => new GetBookQueryResponse
                {
                    Title = b.Title,
                    ISBN = b.ISBN,
                    Price = b.Price
                }).ToList();
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

        public async Task<GetAuthorQueryResponse> GetAuthorById(int id)
        {
            GetAuthorQueryResponse result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get an Author by ID: " + id);


                var author = await _authorRepository.GetAuthorById(id);

                result = new GetAuthorQueryResponse
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Biography = author.Biography
                };
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

        public async Task<IEnumerable<GetAuthorQueryResponse>> GetAuthorsAsync()
        {
            List<GetAuthorQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get Authors");

                var listOfAuthors = await _authorRepository.GetAuthorsAsync();

                result = listOfAuthors.Select(a => new GetAuthorQueryResponse
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Biography = a.Biography
                }).ToList();
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