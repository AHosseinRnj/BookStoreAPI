using MediatR;

namespace Application.Commands.CreateUser
{
    public record CreateUserCommand(int id, string firstName, string lastName, string Address, string Phone) : IRequest<Unit>;
}