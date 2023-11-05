using Application.Repositpries;
using Domain.Entities;
using log4net;
using MediatR;

namespace Application.Commands.CreateBook
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Unit>
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CreateBookHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(CreateBookHandler));
        }

        public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to add a book.");
                await _unitOfWork.BookRepository.AddAsync(request);
            }
            catch (Exception ex)
            {
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