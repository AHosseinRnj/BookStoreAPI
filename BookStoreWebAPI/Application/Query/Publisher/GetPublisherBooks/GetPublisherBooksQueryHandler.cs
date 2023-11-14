using Application.Query.GetBook;
using Application.Services;
using MediatR;

namespace Application.Query.GetPublisherBooks
{
    public class GetPublisherBooksQueryHandler : IRequestHandler<GetPublisherBooksQuery, IEnumerable<GetBookQueryResponse>>
    {
        private readonly IPublisherReadService _publisherService;
        public GetPublisherBooksQueryHandler(IPublisherReadService publisherService)
        {
            _publisherService = publisherService;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetPublisherBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await _publisherService.GetPublisherBooksAsync(request.id);
            return result;
        }
    }
}