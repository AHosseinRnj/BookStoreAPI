using Application.Commands.CreateUser;
using Application.Commands.UpdateUser;
using Application.Query.GetUser;
using Application.Query.GetUserOrders;

namespace Application.Services
{
    public interface IUserService
    {
        Task AddAsync(CreateUserCommand request);
        Task<IEnumerable<GetUserQueryResponse>> GetUsersAsync();
        Task<GetUserQueryResponse> GetUserByIdAsync(int id);
        Task<IEnumerable<GetUserOrdersQueryResponse>> GetUserOrdersById(int id);
        Task UpdateAsync(UpdateUserCommand request);
        Task DeleteByIdAsync(int id);
    }
}