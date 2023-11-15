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
        private readonly IEFUnitOfWork _unitOfWork;
        public AuthorWriteService(IEFUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(AuthorWriteService));
        }

        public async Task AddAsync(CreateAuthorCommand request)
        {
            try
            {
                var author = new Author
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Biography = request.Biography
                };

                await _unitOfWork.AuthorRepository.AddAsync(author);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding an Author: " + ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                await _unitOfWork.AuthorRepository.DeleteByIdAsync(id);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting an Author: " + ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateAsync(UpdateAuthorCommand request)
        {
            try
            {
                var author = new Author
                {
                    Id = request.Id,
                    FirstName = request.Author.FirstName,
                    LastName = request.Author.LastName,
                    Biography = request.Author.Biography
                };

                await _unitOfWork.AuthorRepository.UpdateAsync(author);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating an Author: " + ex.Message, ex);
                throw;
            }
        }
    }
}