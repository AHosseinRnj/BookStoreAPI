using Application.Query.GetUserOrders;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IUserReadRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<GetUserOrderItemQueryResponse>> GetUserOrderItemsById(int id);
    }
}