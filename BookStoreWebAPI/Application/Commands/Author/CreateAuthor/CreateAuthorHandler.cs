using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.CreateAuthor
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Unit>
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CreateAuthorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(CreateAuthorHandler));
        }

        public async Task<Unit> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to add an Author.");
                await _unitOfWork.AuthorRepository.AddAsync(request);;
            }
            catch (Exception ex)
            {
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