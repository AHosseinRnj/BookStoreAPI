using Application.Commands.CreateCategory;
using Application.Commands.UpdateCategory;
using Application.Query.GetBook;
using Application.Query.GetCategory;

namespace Application.Services
{
    public interface ICategoryService
    {
        Task AddAsync(CreateCategoryCommand request);
        Task<IEnumerable<GetCategoryQueryResponse>> GetCategoriesAsync();
        Task<GetCategoryQueryResponse> GetCategoryByIdAsync(int id);
        Task<IEnumerable<GetBookQueryResponse>> GetCategoryBooksAsync(int id);
        Task UpdateAsync(UpdateCategoryCommand request);
        Task DeleteByIdAsync(int id);
    }
}