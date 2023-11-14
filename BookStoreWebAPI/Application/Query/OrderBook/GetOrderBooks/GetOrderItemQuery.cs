using Application.Query.GetOrderBook;
using MediatR;

namespace Application.Query.GetOrderBooks
{
    public record GetOrderItemQuery : IRequest<IEnumerable<GetOrderItemQueryResponse>>;
}