using Domain.Entities;

namespace Application.Repositories
{
    public interface IOrderWriteRepository
    {
        Task AddAsync(Order order);
    }
}