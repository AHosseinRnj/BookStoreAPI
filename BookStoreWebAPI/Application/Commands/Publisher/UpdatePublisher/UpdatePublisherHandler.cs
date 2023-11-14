using Application.Services;
using MediatR;

namespace Application.Commands.UpdatePublisher
{
    public class UpdatePublisherHandler : IRequestHandler<UpdatePublisherCommand, Unit>
    {
        private readonly IPublisherWriteService _publisherService;
        public UpdatePublisherHandler(IPublisherWriteService publisherService)
        {
            _publisherService = publisherService;
        }

        public async Task<Unit> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            await _publisherService.UpdateAsync(request);
            return Unit.Value;
        }
    }
}