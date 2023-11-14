using Application;
using Application.Commands.CreateCategory;
using Application.Commands.UpdateCategory;
using Application.Query.GetBook;
using Application.Query.GetCategory;
using Application.Repositpries;
using Application.Services;
using Domain.Entities;
using log4net;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILog _logger;
        private IDapperUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(IDapperUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _logger = LogManager.GetLogger(typeof(CategoryService));
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

        public async Task<IEnumerable<GetCategoryQueryResponse>> GetCategoriesAsync()
        {
            List<GetCategoryQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get Categories");

                var listOfCategories = await _categoryRepository.GetCategoriesAsync();

                result = listOfCategories.Select(c => new GetCategoryQueryResponse
                {
                    Name = c.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting Categories: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Categories retrieved successfully.");
            return result;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetCategoryBooksAsync(int id)
        {
            List<GetBookQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get a Category's Books by ID: " + id);
                var listOfBooks = await _categoryRepository.GetCategoryBooksAsync(id);

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
                _logger.Error("Error getting an Category's Books: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Category's Books retrieved successfully.");
            return result;
        }

        public async Task<GetCategoryQueryResponse> GetCategoryByIdAsync(int id)
        {
            GetCategoryQueryResponse result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get a Category by ID: " + id);
                var category = await _categoryRepository.GetCategoryByIdAsync(id);

                result = new GetCategoryQueryResponse
                {
                    Name = category.Name
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting a Category: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Category retrieved successfully.");
            return result;
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