using Domain.Entities;

namespace Application.Repositpries
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Book>> GetCategoryBooksAsync(int id);
        Task UpdateAsync(Category category);
        Task DeleteByIdAsync(int id);
    }
}