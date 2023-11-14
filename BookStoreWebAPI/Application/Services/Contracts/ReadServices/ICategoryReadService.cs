using Application.Query.GetBook;
using Application.Query.GetCategory;

namespace Application.Services
{
    public interface ICategoryReadService
    {
        Task<IEnumerable<GetCategoryQueryResponse>> GetCategoriesAsync();
        Task<GetCategoryQueryResponse> GetCategoryByIdAsync(int id);
        Task<IEnumerable<GetBookQueryResponse>> GetCategoryBooksAsync(int id);
    }
}