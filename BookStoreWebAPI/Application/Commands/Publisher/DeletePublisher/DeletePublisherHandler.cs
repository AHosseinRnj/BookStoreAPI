using Application.Services;
using MediatR;

namespace Application.Commands.DeletePublisher
{
    public class DeletePublisherHandler : IRequestHandler<DeletePublisherCommand, Unit>
    {
        private readonly IPublisherService _publisherService;
        public DeletePublisherHandler(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        public async Task<Unit> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            await _publisherService.DeleteByIdAsync(request.id);
            return Unit.Value;
        }
    }
}