using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.DeleteBook
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteBookHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(DeleteBookHandler));
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to delete a book by ID: " + request.id);
                await _unitOfWork.BookRepository.DeleteByIdAsync(request.id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting a book: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Book deleted successfully.");
            return Unit.Value;
        }
    }
}