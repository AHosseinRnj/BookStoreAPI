using MediatR;

namespace Application.Commands.UpdateUser
{
    public record UpdateUserCommand(int id, UpdateUserRequest user) : IRequest<Unit>;
}