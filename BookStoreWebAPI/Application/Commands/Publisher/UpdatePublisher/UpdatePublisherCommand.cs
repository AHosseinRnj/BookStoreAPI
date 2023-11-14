using MediatR;

namespace Application.Commands.UpdatePublisher
{
    public record UpdatePublisherCommand(int Id, UpdatePublisherDto Publisher) : IRequest<Unit>;
}