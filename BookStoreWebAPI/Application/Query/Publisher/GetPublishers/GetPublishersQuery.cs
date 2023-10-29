using Application.Query.GetPublisher;
using MediatR;

namespace Application.Query.GetPublishers
{
    public record GetPublishersQuery : IRequest<IEnumerable<GetPublisherQueryResponse>>;
}