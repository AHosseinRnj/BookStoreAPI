using Application.Query.GetOrderBook;
using Application.Services;
using MediatR;

namespace Application.Query.GetOrderBooks
{
    public class GetOrderBooksQueryHandler : IRequestHandler<GetOrderBooksQuery, IEnumerable<GetOrderBookQueryResponse>>
    {
        private readonly IOrderBookService _orderBookService;
        public GetOrderBooksQueryHandler(IOrderBookService orderBookService)
        {
            _orderBookService = orderBookService;
        }

        public async Task<IEnumerable<GetOrderBookQueryResponse>> Handle(GetOrderBooksQuery request, CancellationToken cancellationToken)
        {
            var orderBooks = await _orderBookService.GetOrderBooksAsync();
            return orderBooks;
        }
    }
}