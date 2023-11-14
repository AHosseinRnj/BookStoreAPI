using Application.Commands.CreateCategory;
using Application.Commands.UpdateCategory;
using Application.Repositories;
using Domain.Entities;
using Infrastructure.Services;
using log4net;

namespace Application.Services
{
    public class CategoryWriteService : ICategoryWriteService
    {
        private readonly ILog _logger;
        private IDapperUnitOfWork _unitOfWork;
        private readonly ICategoryWriteRepository _categoryRepository;
        public CategoryWriteService(IDapperUnitOfWork unitOfWork, ICategoryWriteRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _logger = LogManager.GetLogger(typeof(CategoryWriteService));
        }

        public async Task AddAsync(CreateCategoryCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add a Category.");

                var category = new Category
                {
                    Name = request.Name
                };

                await _categoryRepository.AddAsync(category);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error adding a Category: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Category added successfully.");
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to delete a Category by ID: " + id);

                await _categoryRepository.DeleteByIdAsync(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error deleting a Category: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Category deleted successfully.");
        }

        public async Task UpdateAsync(UpdateCategoryCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to update a Category.");

                var category = new Category
                {
                    Id = request.Id,
                    Name = request.Category.Name
                };

                await _categoryRepository.UpdateAsync(category);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error updating a Category: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Category updated successfully.");
        }
    }
}