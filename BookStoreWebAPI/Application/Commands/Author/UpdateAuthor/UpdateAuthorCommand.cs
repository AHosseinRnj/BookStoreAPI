using MediatR;

namespace Application.Commands.UpdateAuthor
{
    public record UpdateAuthorCommand(int Id, UpdateAuthorDto Author) : IRequest<Unit>; 
}