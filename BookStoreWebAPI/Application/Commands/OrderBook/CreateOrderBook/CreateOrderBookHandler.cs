using Application.Services;
using MediatR;

namespace Application.Commands.CreateOrderBook
{
    public class CreateOrderBookHandler : IRequestHandler<CreateOrderBookCommand, Unit>
    {
        private readonly IOrderBookService _orderBookService;
        public CreateOrderBookHandler(IOrderBookService orderBookService)
        {
            _orderBookService = orderBookService;
        }

        public async Task<Unit> Handle(CreateOrderBookCommand request, CancellationToken cancellationToken)
        {
            await _orderBookService.AddAsync(request);
            return Unit.Value;
        }
    }
}