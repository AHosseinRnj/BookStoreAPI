using MediatR;

namespace Application.Commands.UpdateBook
{
    public record UpdateBookCommand(int Id, UpdateBookDto Book) : IRequest; 
}