using Application.Commands.CreateUser;
using Application.Commands.UpdateUser;

namespace Application.Services
{
    public interface IUserWriteService
    {
        Task AddAsync(CreateUserCommand request);
        Task UpdateAsync(UpdateUserCommand request);
        Task DeleteByIdAsync(int id);
    }
}