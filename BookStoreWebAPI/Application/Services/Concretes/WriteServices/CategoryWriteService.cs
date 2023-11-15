using Application.Commands.CreateCategory;
using Application.Commands.UpdateCategory;
using Application.Repositories;
using Domain.Entities;
using log4net;

namespace Application.Services
{
    public class CategoryWriteService : ICategoryWriteService
    {
        private readonly ILog _logger;
        private IEFUnitOfWork _unitOfWork;
        public CategoryWriteService(IEFUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(CategoryWriteService));
        }

        public async Task AddAsync(CreateCategoryCommand request)
        {
            try
            {
                var category = new Category
                {
                    Name = request.Name
                };

                await _unitOfWork.CategoryRepository.AddAsync(category);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding a Category: " + ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                await _unitOfWork.CategoryRepository.DeleteByIdAsync(id);
                 await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting a Category: " + ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateAsync(UpdateCategoryCommand request)
        {
            try
            {
                var category = new Category
                {
                    Id = request.Id,
                    Name = request.Category.Name
                };

                await _unitOfWork.CategoryRepository.UpdateAsync(category);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating a Category: " + ex.Message, ex);
                throw;
            }
        }
    }
}