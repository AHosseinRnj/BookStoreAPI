using Application.Query.GetBook;
using Application.Query.GetCategory;
using Application.Repositories;
using log4net;

namespace Application.Services
{
    public class CategoryReadService : ICategoryReadService
    {
        private readonly ILog _logger;
        private IDapperUnitOfWork _unitOfWork;
        private readonly ICategoryReadRepository _categoryRepository;
        public CategoryReadService(IDapperUnitOfWork unitOfWork, ICategoryReadRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _logger = LogManager.GetLogger(typeof(CategoryReadService));
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
    }
}