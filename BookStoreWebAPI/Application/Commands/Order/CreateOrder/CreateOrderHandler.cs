using Application.Command.CreateOrder;
using Application.Services;
using MediatR;

namespace Application.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private readonly IOrderService _orderService;
        public CreateOrderHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderService.AddAsync(request);
            return Unit.Value;
        }
    }
}