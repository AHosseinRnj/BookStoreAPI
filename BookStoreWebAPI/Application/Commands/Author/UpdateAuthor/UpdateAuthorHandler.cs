using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.UpdateAuthor
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _uniteOfWork;
        public UpdateAuthorHandler(IUnitOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
            _logger = LogManager.GetLogger(typeof(UpdateAuthorHandler));
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to update an Author.");
                await _uniteOfWork.AuthorRepository.UpdateAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating an Author: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _uniteOfWork.Commit();
            }

            _logger.Info("Author updated successfully.");
            return Unit.Value;
        }
    }
}