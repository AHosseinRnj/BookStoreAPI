using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.UpdateAuthor
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IAuthorRepository _authorRepository;
        public UpdateAuthorHandler(IUnitOfWork uniteOfWork, IAuthorRepository authorRepository)
        {
            _uniteOfWork = uniteOfWork;
            _authorRepository = authorRepository;
            _logger = LogManager.GetLogger(typeof(UpdateAuthorHandler));
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _uniteOfWork.BeginTransaction();
                _logger.Info("Received a request to update an Author.");
                await _authorRepository.UpdateAsync(request);
            }
            catch (Exception ex)
            {
                _uniteOfWork.Rollback();
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