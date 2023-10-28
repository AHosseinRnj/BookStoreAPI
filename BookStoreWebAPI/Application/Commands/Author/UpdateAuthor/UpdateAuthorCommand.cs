using MediatR;

namespace Application.Commands.UpdateAuthor
{
    public record UpdateAuthorCommand(int id, string FirstName, string LastName, string Description) : IRequest<Unit>; 
}