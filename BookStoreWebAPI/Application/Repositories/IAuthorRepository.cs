using Domain.Entities;

namespace Application.Repositories
{
    public interface IAuthorRepository
    {
        Task AddAsync(Author entity);
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Author entity);
    }
}