using Application.Query.GetOrderBook;
using MediatR;

namespace Application.Query.GetOrderBooks
{
    public record GetOrderBooksQuery : IRequest<IEnumerable<GetOrderBookQueryResponse>>;
}