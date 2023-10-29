using MediatR;

namespace Application.Commands.UpdatePublisher
{
    public record UpdatePublisherCommand(int Id, string Name, string Biography) : IRequest<Unit>;
}