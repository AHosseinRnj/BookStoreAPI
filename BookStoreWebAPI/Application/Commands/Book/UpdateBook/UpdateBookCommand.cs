using MediatR;

namespace Application.Commands.UpdateBook
{
    public record UpdateBookCommand(int id, string Title, string ISBN, double price) : IRequest; 
}