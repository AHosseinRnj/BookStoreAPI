using Domain.Entities;

namespace Application.Repositories
{
    public interface IAuthorReadRepository
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorById(int id);
        Task<IEnumerable<Book>> GetAuthorBooksAsync(int id);
    }
}