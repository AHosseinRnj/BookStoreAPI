using Domain.Entities;

namespace Application.Repositories
{
    public interface IOrderItemWriteRepository
    {
        Task AddAsync(OrderItem orderBook);
    }
}