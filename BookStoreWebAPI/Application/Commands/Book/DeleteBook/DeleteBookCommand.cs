using MediatR;

namespace Application.Commands.DeleteBook
{
    public record DeleteBookCommand(int id) : IRequest;
}