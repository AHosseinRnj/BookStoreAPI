using Application.Commands.CreateUser;
using Application.Commands.UpdateUser;
using Application.Query.GetUser;
using Application.Query.GetUserOrders;

namespace Application.Repositpries
{
    public interface IUserRepository
    {
        Task AddAsync(CreateUserCommand user);
        Task<IEnumerable<GetUserQueryResponse>> GetUsersAsync();
        Task<GetUserQueryResponse> GetUserByIdAsync(int id);
        Task<IEnumerable<GetUserOrdersQueryResponse>> GetUserOrdersById(int id);
        Task UpdateAsync(UpdateUserCommand request);
        Task DeleteByIdAsync(int id);
    }
}