using Domain.Entities;

namespace Application.Repositpries
{
    public interface IAuthorRepository
    {
        Task AddAsync(Author author);
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorById(int id);
        Task<IEnumerable<Book>> GetAuthorBooksAsync(int id);
        Task UpdateAsync(Author author);
        Task DeleteByIdAsync(int id);
    }
}