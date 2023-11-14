using MediatR;

namespace Application.Query.GetUserOrders
{
    public record GetUserOrderItemQuery(int id) : IRequest<IEnumerable<GetUserOrderItemQueryResponse>>;
}