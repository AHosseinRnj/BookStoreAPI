using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.CreateAuthor
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Unit>
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorRepository _authorRepository;
        public CreateAuthorHandler(IUnitOfWork unitOfWork, IAuthorRepository authorRepository)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
            _logger = LogManager.GetLogger(typeof(CreateAuthorHandler));
        }

        public async Task<Unit> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add an Author.");
                await _authorRepository.AddAsync(request); ;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error adding an Author: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Author added successfully.");
            return Unit.Value;
        }
    }
}