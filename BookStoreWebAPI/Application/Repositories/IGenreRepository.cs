using Domain.Entities;

namespace Application.Repositories
{
    public interface IGenreRepository {
        Task AddAsync(Genre entity);
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetByIdAsync(int id);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Genre entity);
    }
}