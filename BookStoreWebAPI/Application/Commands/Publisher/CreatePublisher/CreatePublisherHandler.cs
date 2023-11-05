using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.CreatePublisher
{
    public class CreatePublisherHandler : IRequestHandler<CreatePublisherCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public CreatePublisherHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(CreatePublisherHandler));
        }

        public async Task<Unit> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to add a Publisher.");
                await _unitOfWork.PublisherRepository.AddAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding a Publisher: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Publisher added successfully.");
            return Unit.Value;
        }
    }
}