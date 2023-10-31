using Application.Repositpries;
using MediatR;

namespace Application.Commands.CreateOrderBook
{
    public class CreateOrderBookHandler : IRequestHandler<CreateOrderBookCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        public CreateOrderBookHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateOrderBookCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.OrderBookRepository.AddAsync(request);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}