using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.CreatePublisher
{
    public class CreatePublisherHandler : IRequestHandler<CreatePublisherCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IPublisherRepository _publisherRepository;
        public CreatePublisherHandler(IUnitOfWork unitOfWork, IPublisherRepository publisherRepository)
        {
            _unitOfWork = unitOfWork;
            _publisherRepository = publisherRepository;
            _logger = LogManager.GetLogger(typeof(CreatePublisherHandler));
        }

        public async Task<Unit> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add a Publisher.");
                await _publisherRepository.AddAsync(request);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
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