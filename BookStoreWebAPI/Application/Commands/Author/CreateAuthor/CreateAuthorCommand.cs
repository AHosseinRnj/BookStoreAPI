using MediatR;

namespace Application.Commands.CreateAuthor
{
    public record CreateAuthorCommand(string FirstName, string LastName, string Biography) : IRequest<Unit>;
}