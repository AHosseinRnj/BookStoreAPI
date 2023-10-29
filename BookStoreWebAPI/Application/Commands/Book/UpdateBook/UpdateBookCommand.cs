using MediatR;

namespace Application.Commands.UpdateBook
{
    public record UpdateBookCommand(int id, string Title, string ISBN, double price, int AuthorId, int PublisherId, int CategoryId) : IRequest; 
}