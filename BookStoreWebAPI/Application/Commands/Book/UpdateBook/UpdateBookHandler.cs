using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.UpdateBook
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IBookRepository _bookRepository;
        public UpdateAuthorHandler(IUnitOfWork uniteOfWork, IBookRepository bookRepository)
        {
            _uniteOfWork = uniteOfWork;
            _bookRepository = bookRepository;
            _logger = LogManager.GetLogger(typeof(UpdateAuthorHandler));
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _uniteOfWork.BeginTransaction();
                _logger.Info("Received a request to update a book.");
                await _bookRepository.UpdateAsync(request);
            }
            catch (Exception ex)
            {
                _uniteOfWork.Rollback();
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