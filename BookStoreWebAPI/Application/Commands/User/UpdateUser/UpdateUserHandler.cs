using Application.Repositpries;
using MediatR;

namespace Application.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        public UpdateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.UserRepository.UpdateAsync(request);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}