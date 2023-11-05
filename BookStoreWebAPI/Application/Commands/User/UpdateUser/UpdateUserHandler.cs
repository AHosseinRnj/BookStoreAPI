using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public UpdateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(UpdateUserHandler));
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to update a User.");
                await _unitOfWork.UserRepository.UpdateAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating a User: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("User updated successfully.");
            return Unit.Value;
        }
    }
}