using Application.Repositpries;
using MediatR;

namespace Application.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        public CreateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.UserRepository.AddAsync(request);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}