using Domain.Entities;

namespace Application.Repositpries
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task UpdateAsync(Book book);
        Task DeleteByIdAsync(int id);
    }
}