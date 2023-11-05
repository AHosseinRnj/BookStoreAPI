using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.UpdatePublisher
{
    public class UpdatePublisherHandler : IRequestHandler<UpdatePublisherCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public UpdatePublisherHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(UpdatePublisherHandler));

        }

        public async Task<Unit> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to update a Publisher.");
                await _unitOfWork.PublisherRepository.UpdateAsync(request);
            }
            catch (Exception ex)
            {
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