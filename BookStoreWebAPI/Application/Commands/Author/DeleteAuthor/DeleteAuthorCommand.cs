using MediatR;

namespace Application.Commands.DeleteAuthor
{
    public record DeleteAuthorCommand(int Id) : IRequest<Unit>;
}