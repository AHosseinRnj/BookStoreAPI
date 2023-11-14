using Application.Commands.CreateAuthor;
using Application.Commands.UpdateAuthor;
using Application.Repositories;
using Domain.Entities;
using log4net;

namespace Application.Services
{
    public class AuthorWriteService : IAuthorWriteService
    {
        private readonly ILog _logger;
        private readonly IDapperUnitOfWork _unitOfWork;
        private readonly IAuthorWriteRepository _authorRepository;
        public AuthorWriteService(IDapperUnitOfWork unitOfWork, IAuthorWriteRepository authorRepository)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
            _logger = LogManager.GetLogger(typeof(AuthorWriteService));
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