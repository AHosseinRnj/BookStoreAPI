using Application.Services;
using MediatR;

namespace Application.Commands.CreatePublisher
{
    public class CreatePublisherHandler : IRequestHandler<CreatePublisherCommand, Unit>
    {
        private readonly IPublisherWriteService _publisherService;
        public CreatePublisherHandler(IPublisherWriteService publisherService)
        {
            _publisherService = publisherService;
        }

        public async Task<Unit> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            await _publisherService.AddAsync(request);
            return Unit.Value;
        }
    }
}