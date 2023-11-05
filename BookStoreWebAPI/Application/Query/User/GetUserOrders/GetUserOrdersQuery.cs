using MediatR;

namespace Application.Query.GetUserOrders
{
    public record GetUserOrdersQuery(int id) : IRequest<IEnumerable<GetUserOrdersQueryResponse>>;
}