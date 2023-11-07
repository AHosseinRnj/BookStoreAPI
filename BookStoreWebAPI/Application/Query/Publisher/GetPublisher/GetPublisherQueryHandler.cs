using Application.Services;
using MediatR;

namespace Application.Query.GetPublisher
{
    public class GetPublisherQueryHandler : IRequestHandler<GetPublisherQuery, GetPublisherQueryResponse>
    {
        private readonly IPublisherService _publisherService;
        public GetPublisherQueryHandler(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        public async Task<GetPublisherQueryResponse> Handle(GetPublisherQuery request, CancellationToken cancellationToken)
        {
            var result = await _publisherService.GetPublisherByIdAsync(request.id);
            return result;
        }
    }
}