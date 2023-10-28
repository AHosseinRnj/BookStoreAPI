using MediatR;

namespace Application.Commands.DeleteAuthor
{
    public record DeleteAuthorCommand(int id) : IRequest<Unit>;
}