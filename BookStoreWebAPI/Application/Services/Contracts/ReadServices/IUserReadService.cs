using Application.Query.GetUser;
using Application.Query.GetUserOrders;

namespace Application.Services
{
    public interface IUserReadService
    {
        Task<IEnumerable<GetUserQueryResponse>> GetUsersAsync();
        Task<GetUserQueryResponse> GetUserByIdAsync(int id);
        Task<IEnumerable<GetUserOrderItemQueryResponse>> GetUserOrdersById(int id);
    }
}