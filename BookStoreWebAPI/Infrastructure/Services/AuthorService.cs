using Application;
using Application.Commands.CreateAuthor;
using Application.Commands.UpdateAuthor;
using Application.Query.Author.GetAuthor;
using Application.Query.GetBook;
using Application.Repositpries;
using Application.Services;
using Azure.Core;
using Domain.Entities;
using Infrastructure.Persistance;
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
                    Id = request.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Description = request.Description
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
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetAuthorBooksAsync(int id)
        {
            var listOfAuthorBooks = await _authorRepository.GetAuthorBooksAsync(id);

            var result = listOfAuthorBooks.Select(b => new GetBookQueryResponse
            {
                Title = b.Title,
                ISBN = b.ISBN,
                Price = b.Price
            });

            return result;
        }

        public async Task<GetAuthorQueryResponse> GetAuthorById(int id)
        {
            var author = await _authorRepository.GetAuthorById(id);

            var authorReponse = new GetAuthorQueryResponse
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                Description = author.Description
            };

            return authorReponse;
        }

        public async Task<IEnumerable<GetAuthorQueryResponse>> GetAuthorsAsync()
        {
            var listOfAuthors = await _authorRepository.GetAuthorsAsync();

            var result = listOfAuthors.Select(a => new GetAuthorQueryResponse
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                Description = a.Description
            });

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
                    Id = request.id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Description = request.Description
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