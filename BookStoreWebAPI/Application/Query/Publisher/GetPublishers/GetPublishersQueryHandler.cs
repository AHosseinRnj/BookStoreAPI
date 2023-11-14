using Application.Query.GetPublisher;
using Application.Services;
using MediatR;

namespace Application.Query.GetPublishers
{
    public class GetPublishersQueryHandler : IRequestHandler<GetPublishersQuery, IEnumerable<GetPublisherQueryResponse>>
    {
        private readonly IPublisherReadService _publisherService;
        public GetPublishersQueryHandler(IPublisherReadService publisherService)
        {
            _publisherService = publisherService;
        }

        public async Task<IEnumerable<GetPublisherQueryResponse>> Handle(GetPublishersQuery request, CancellationToken cancellationToken)
        {
            var result = await _publisherService.GetPublishersAsync();
            return result;
        }
    }
}