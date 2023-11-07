using Application.Query.GetOrder;
using Application.Services;
using MediatR;

namespace Application.Query.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<GetOrderQueryResponse>>
    {
        private readonly IOrderService _orderService;
        public GetOrdersQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IEnumerable<GetOrderQueryResponse>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var listOfOrders = await _orderService.GetOrdersAsync();
            return listOfOrders;
        }
    }
}