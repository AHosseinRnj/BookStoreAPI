using Application.Services;
using MediatR;

namespace Application.Commands.CreateOrderBook
{
    public class CreateOrderItemHandler : IRequestHandler<CreateOrderItemCommand, Unit>
    {
        private readonly IOrderBookService _orderBookService;
        public CreateOrderItemHandler(IOrderBookService orderBookService)
        {
            _orderBookService = orderBookService;
        }

        public async Task<Unit> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            await _orderBookService.AddAsync(request);
            return Unit.Value;
        }
    }
}