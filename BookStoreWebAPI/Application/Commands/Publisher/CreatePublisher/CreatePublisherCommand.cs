using MediatR;

namespace Application.Commands.CreatePublisher
{
    public record CreatePublisherCommand(string Name, string Description) : IRequest<Unit>;
}