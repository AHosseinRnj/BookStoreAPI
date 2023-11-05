using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.DeletePublisher
{
    public class DeletePublisherHandler : IRequestHandler<DeletePublisherCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public DeletePublisherHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(DeletePublisherHandler));
        }

        public async Task<Unit> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to delete a Publisher by ID: " + request.id);
                await _unitOfWork.PublisherRepository.DeleteByIdAsync(request.id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting a Publisher: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Publisher deleted successfully.");
            return Unit.Value;
        }
    }
}