using MediatR;

namespace Application.Query.GetOrder
{
    public record GetOrderQuery(int id) : IRequest<GetOrderQueryResponse>;
}