using MediatR;

namespace Application.Query.GetPublisher
{
    public record GetPublisherQuery(int id) : IRequest<GetPublisherQueryResponse>;
}