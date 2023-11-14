using Domain.Entities;

namespace Application.Repositories
{
    public interface IAuthorWriteRepository
    {
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteByIdAsync(int id);
    }
}