using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.UpdatePublisher
{
    public class UpdatePublisherHandler : IRequestHandler<UpdatePublisherCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IPublisherRepository _publisherRepository;
        public UpdatePublisherHandler(IUnitOfWork unitOfWork, IPublisherRepository publisherRepository)
        {
            _unitOfWork = unitOfWork;
            _publisherRepository = publisherRepository;
            _logger = LogManager.GetLogger(typeof(UpdatePublisherHandler));
        }

        public async Task<Unit> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to update a Publisher.");
                await _publisherRepository.UpdateAsync(request);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error updating a Publisher: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Publisher updated successfully.");
            return Unit.Value;
        }
    }
}