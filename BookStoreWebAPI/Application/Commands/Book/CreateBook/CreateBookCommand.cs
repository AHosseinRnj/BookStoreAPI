using MediatR;

namespace Application.Commands.CreateBook
{
    public record CreateBookCommand(string Title, int Quantity, string ISBN, double Price, int AuthorId, int PublisherId, int CategoryId) : IRequest<Unit>;
}