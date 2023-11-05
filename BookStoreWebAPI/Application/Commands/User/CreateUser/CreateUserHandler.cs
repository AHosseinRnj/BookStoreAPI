using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public CreateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(CreateUserHandler));
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to add a User.");
                await _unitOfWork.UserRepository.AddAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding a User: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("User added successfully.");
            return Unit.Value;
        }
    }
}