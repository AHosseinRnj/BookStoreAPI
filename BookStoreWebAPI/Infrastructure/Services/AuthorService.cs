using Application;
using Application.Commands.CreateAuthor;
using Application.Commands.UpdateAuthor;
using Application.Query.Author.GetAuthor;
using Application.Query.GetBook;
using Application.Repositpries;
using Application.Services;
using Domain.Entities;
using log4net;

namespace Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IUnitOfWork unitOfWork, IAuthorRepository authorRepository)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
            _logger = LogManager.GetLogger(typeof(AuthorService));
        }

        public async Task AddAsync(CreateAuthorCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add an Author.");

                var author = new Author
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Biography = request.Biography
                };

                await _authorRepository.AddAsync(author);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error adding an Author: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Author added successfully.");
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to delete a Author by ID: " + id);

                await _authorRepository.DeleteByIdAsync(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error deleting an Author: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Author Deleted successfully.");
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

        public async Task UpdateAsync(UpdateAuthorCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to update an Author.");

                var author = new Author
                {
                    Id = request.Id,
                    FirstName = request.Author.FirstName,
                    LastName = request.Author.LastName,
                    Biography = request.Author.Biography
                };

                await _authorRepository.UpdateAsync(author);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error updating an Author: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Author updated successfully.");
        }
    }
}