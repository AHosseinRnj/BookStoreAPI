using Application.Commands.CreateCategory;
using Application.Commands.UpdateCategory;

namespace Application.Services
{
    public interface ICategoryWriteService
    {
        Task AddAsync(CreateCategoryCommand request);
        Task UpdateAsync(UpdateCategoryCommand request);
        Task DeleteByIdAsync(int id);
    }
}