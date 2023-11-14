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
            var listOfCategories = await _categoryRepository.GetCategoriesAsync();

            var result = listOfCategories.Select(c => new GetCategoryQueryResponse
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return result;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetCategoryBooksAsync(int id)
        {
            var listOfBooks = await _categoryRepository.GetCategoryBooksAsync(id);

            var result = listOfBooks.Select(b => new GetBookQueryResponse
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                Price = b.Price,
                Quantity = b.Quantity,
            }).ToList();

            return result;
        }

        public async Task<GetCategoryQueryResponse> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            var result = new GetCategoryQueryResponse
            {
                Id = category.Id,
                Name = category.Name
            };

            return result;
        }
    }
}