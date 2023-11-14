using MediatR;

namespace Application.Commands.UpdateUser
{
    public record UpdateUserCommand(int Id, UpdateUserRequest User) : IRequest<Unit>;
}