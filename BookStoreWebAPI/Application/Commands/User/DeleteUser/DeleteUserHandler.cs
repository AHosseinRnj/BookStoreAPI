using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public DeleteUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(DeleteUserHandler));
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to delete a User by ID: " + request.id);
                await _unitOfWork.UserRepository.DeleteByIdAsync(request.id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting a User: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("User deleted successfully.");
            return Unit.Value;
        }
    }
}