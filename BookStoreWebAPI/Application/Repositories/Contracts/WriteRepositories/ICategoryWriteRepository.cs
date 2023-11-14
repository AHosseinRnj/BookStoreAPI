using Domain.Entities;

namespace Application.Repositories
{
    public interface ICategoryWriteRepository
    {
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteByIdAsync(int id);
    }
}