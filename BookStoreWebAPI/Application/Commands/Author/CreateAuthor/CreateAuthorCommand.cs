using MediatR;

namespace Application.Commands.CreateAuthor
{
    public record CreateAuthorCommand(int Id, string FirstName, string LastName, string Description) : IRequest<Unit>;
}