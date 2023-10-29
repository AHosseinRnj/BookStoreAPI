using MediatR;

namespace Application.Commands.DeletePublisher
{
    public record DeletePublisherCommand(int id) : IRequest<Unit>;
}