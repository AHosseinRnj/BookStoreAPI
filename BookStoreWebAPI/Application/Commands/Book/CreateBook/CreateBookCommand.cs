using MediatR;

namespace Application.Commands.CreateBook
{
    public record CreateBookCommand(int Id,string Title, string ISBN, double Price, int AuthorId, int PublisherId) : IRequest<Unit>;
}