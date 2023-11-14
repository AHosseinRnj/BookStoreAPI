using Domain.Entities;

namespace Application.Repositories
{
    public interface IUserWriteRepository
    {
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteByIdAsync(int id);
    }
}