using Domain.Entities;

namespace Application.Repositories
{
    public interface IBookReadRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
    }
}