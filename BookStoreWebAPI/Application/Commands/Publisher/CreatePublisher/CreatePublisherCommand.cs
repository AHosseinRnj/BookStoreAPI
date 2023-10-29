using MediatR;

namespace Application.Commands.CreatePublisher
{
    public record CreatePublisherCommand(int Id, string Name, string Biography) : IRequest<Unit>;
}