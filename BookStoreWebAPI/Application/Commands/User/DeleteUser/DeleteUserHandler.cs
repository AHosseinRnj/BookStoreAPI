using Application.Services;
using MediatR;

namespace Application.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserWriteService _userService;
        public DeleteUserHandler(IUserWriteService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.DeleteByIdAsync(request.id);
            return Unit.Value;
        }
    }
}