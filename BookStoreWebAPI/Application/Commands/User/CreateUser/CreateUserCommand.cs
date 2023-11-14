using MediatR;

namespace Application.Commands.CreateUser
{
    public record CreateUserCommand(string FirstName, string LastName, string Address, string Phone) : IRequest<Unit>;
}