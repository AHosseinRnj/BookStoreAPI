using Application.Query.GetOrder;
using MediatR;

namespace Application.Query.GetOrders
{
    public record GetOrdersQuery : IRequest<IEnumerable<GetOrderQueryResponse>>;
}