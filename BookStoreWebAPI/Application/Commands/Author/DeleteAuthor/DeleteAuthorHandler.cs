using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.DeleteAuthor
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAuthorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(DeleteAuthorHandler));
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to delete a Author by ID: " + request.id);
                await _unitOfWork.AuthorRepository.DeleteByIdAsync(request.id);

            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting an Author: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Author deleted successfully.");
            return Unit.Value;
        }
    }
}