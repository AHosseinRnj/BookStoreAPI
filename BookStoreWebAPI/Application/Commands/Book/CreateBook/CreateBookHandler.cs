using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.CreateBook
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Unit>
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        public CreateBookHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _logger = LogManager.GetLogger(typeof(CreateBookHandler));
        }

        public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add a book.");
                await _bookRepository.AddAsync(request);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error adding a book: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Book added successfully.");
            return Unit.Value;
        }
    }
}