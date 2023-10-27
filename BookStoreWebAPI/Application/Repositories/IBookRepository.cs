using Domain.Entities;

namespace Application.Repositories
{
    public interface IBookRepository {
        Task AddAsync(Book entity);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, Book entity);
    }
}