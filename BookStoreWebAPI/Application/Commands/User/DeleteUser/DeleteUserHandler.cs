using Application.Repositpries;
using MediatR;

namespace Application.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        public DeleteUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.UserRepository.DeleteByIdAsync(request.id);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}