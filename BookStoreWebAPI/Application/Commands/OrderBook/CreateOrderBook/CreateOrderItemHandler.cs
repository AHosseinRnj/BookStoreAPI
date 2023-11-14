using Application.Services;
using MediatR;

namespace Application.Commands.CreateOrderBook
{
    public class CreateOrderItemHandler : IRequestHandler<CreateOrderItemCommand, Unit>
    {
        private readonly IOrderItemWriteService _orderItemService;
        public CreateOrderItemHandler(IOrderItemWriteService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        public async Task<Unit> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            await _orderItemService.AddAsync(request);
            return Unit.Value;
        }
    }
}