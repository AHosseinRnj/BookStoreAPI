using Domain.Entities;

namespace Application.Repositories
{
    public interface IPublisherWriteRepository
    {
        Task AddAsync(Publisher publisher);
        Task UpdateAsync(Publisher publisher);
        Task DeleteByIdAsync(int id);
    }
}