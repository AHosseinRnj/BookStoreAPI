using MediatR;

namespace Application.Commands.DeleteUser
{
    public record DeleteUserCommand(int id) : IRequest<Unit>;
}