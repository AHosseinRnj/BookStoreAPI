using Domain.Entities;

namespace Application.Repositpries
{
    public interface IPublisherRepository
    {
        Task AddAsync(Publisher publisher);
        Task<IEnumerable<Publisher>> GetPublishersAsync();
        Task<Publisher> GetPublisherByIdAsync(int id);
        Task<IEnumerable<Book>> GetPublisherBooksAsync(int id);
        Task UpdateAsync(Publisher publisher);
        Task DeleteByIdAsync(int id);
    }
}