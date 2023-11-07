using Application.Services;
using MediatR;

namespace Application.Query.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, GetOrderQueryResponse>
    {
        private readonly IOrderService _orderService;
        public GetOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetOrderQueryResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderByIdAsync(request.id);
            return order;
        }
    }
}