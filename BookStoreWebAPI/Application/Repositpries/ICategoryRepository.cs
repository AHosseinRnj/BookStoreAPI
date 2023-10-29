using Application.Commands.CreateCategory;
using Application.Commands.UpdateCategory;
using Application.Query.GetBook;
using Application.Query.GetCategory;

namespace Application.Repositpries
{
    public interface ICategoryRepository
    {
        Task AddAsync(CreateCategoryCommand category);
        Task<IEnumerable<GetCategoryQueryResponse>> GetCategoriesAsync();
        Task<GetCategoryQueryResponse> GetCategoryByIdAsync(int id);
        Task<IEnumerable<GetBookQueryResponse>> GetCategoryBooksAsync(int id);
        Task UpdateAsync(UpdateCategoryCommand category);
        Task DeleteByIdAsync(int id);
    }
}