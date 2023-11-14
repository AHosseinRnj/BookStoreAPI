using Domain.Entities;

namespace Application.Repositories
{
    public interface ICategoryReadRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Book>> GetCategoryBooksAsync(int id);
    }
}