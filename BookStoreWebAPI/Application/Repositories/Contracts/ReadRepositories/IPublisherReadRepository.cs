using Domain.Entities;

namespace Application.Repositories
{
    public interface IPublisherReadRepository
    {
        Task<IEnumerable<Publisher>> GetPublishersAsync();
        Task<Publisher> GetPublisherByIdAsync(int id);
        Task<IEnumerable<Book>> GetPublisherBooksAsync(int id);
    }
}