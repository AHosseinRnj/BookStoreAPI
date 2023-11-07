using Application.Query.GetUser;
using Application.Services;
using MediatR;

namespace Application.Query.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<GetUserQueryResponse>>
    {
        private readonly IUserService _userService;
        public GetUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<GetUserQueryResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var listOfUsers = await _userService.GetUsersAsync();
            return listOfUsers;
        }
    }
}