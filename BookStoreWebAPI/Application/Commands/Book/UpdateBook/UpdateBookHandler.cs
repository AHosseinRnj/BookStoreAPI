using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.UpdateBook
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _uniteOfWork;
        public UpdateAuthorHandler(IUnitOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
            _logger = LogManager.GetLogger(typeof(UpdateAuthorHandler));
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to update a book.");
                await _uniteOfWork.BookRepository.UpdateAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating a book: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _uniteOfWork.Commit();
            }

            _logger.Info("Book updated successfully.");
            return Unit.Value;
        }
    }
}