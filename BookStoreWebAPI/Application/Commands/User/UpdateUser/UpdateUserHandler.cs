using Application.Services;
using MediatR;

namespace Application.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserWriteService _userService;
        public UpdateUserHandler(IUserWriteService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.UpdateAsync(request);
            return Unit.Value;
        }
    }
}