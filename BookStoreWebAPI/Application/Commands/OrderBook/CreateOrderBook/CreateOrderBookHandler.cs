using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.CreateOrderBook
{
    public class CreateOrderBookHandler : IRequestHandler<CreateOrderBookCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public CreateOrderBookHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(CreateOrderBookHandler));
        }

        public async Task<Unit> Handle(CreateOrderBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to add an OrderBook.");
                await _unitOfWork.OrderBookRepository.AddAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding an OrderBook: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("OrderBook added successfully.");
            return Unit.Value;
        }
    }
}