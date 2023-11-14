using Application.Query.GetOrderBook;
using Application.Services;
using MediatR;

namespace Application.Query.GetOrderBooks
{
    public class GetOrderItemQueryHandler : IRequestHandler<GetOrderItemQuery, IEnumerable<GetOrderItemQueryResponse>>
    {
        private readonly IOrderItemReadService _orderBookService;
        public GetOrderItemQueryHandler(IOrderItemReadService orderBookService)
        {
            _orderBookService = orderBookService;
        }

        public async Task<IEnumerable<GetOrderItemQueryResponse>> Handle(GetOrderItemQuery request, CancellationToken cancellationToken)
        {
            var orderBooks = await _orderBookService.GetOrderBooksAsync();
            return orderBooks;
        }
    }
}