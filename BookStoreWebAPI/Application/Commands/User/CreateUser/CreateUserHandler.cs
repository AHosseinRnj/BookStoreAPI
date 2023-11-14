using Application.Services;
using MediatR;

namespace Application.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUserWriteService _userService;
        public CreateUserHandler(IUserWriteService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.AddAsync(request);
            return Unit.Value;
        }
    }
}