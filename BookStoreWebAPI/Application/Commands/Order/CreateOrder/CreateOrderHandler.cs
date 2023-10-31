using Application.Command.CreateOrder;
using Application.Repositpries;
using MediatR;

namespace Application.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        public CreateOrderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.OrderRepository.AddAsync(request);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}