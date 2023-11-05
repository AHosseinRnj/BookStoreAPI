using Application.Command.CreateOrder;
using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public CreateOrderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(CreateOrderHandler));
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to add an Order.");
                await _unitOfWork.OrderRepository.AddAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding an Order: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Order added successfully.");
            return Unit.Value;
        }
    }
}