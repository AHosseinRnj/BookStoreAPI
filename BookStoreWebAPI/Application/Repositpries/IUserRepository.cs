using Application.Query.GetUserOrders;
using Domain.Entities;

namespace Application.Repositpries
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<GetUserOrderItemQueryResponse>> GetUserOrderItemsById(int id);
        Task UpdateAsync(User user);
        Task DeleteByIdAsync(int id);
    }
}