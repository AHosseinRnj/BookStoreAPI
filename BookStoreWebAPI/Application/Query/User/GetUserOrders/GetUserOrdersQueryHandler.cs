using Application.Services;
using MediatR;

namespace Application.Query.GetUserOrders
{
    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, IEnumerable<GetUserOrdersQueryResponse>>
    {
        private readonly IUserService _userService;
        public GetUserOrdersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<GetUserOrdersQueryResponse>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            var listOfOrders = await _userService.GetUserOrdersById(request.id);
            return listOfOrders;
        }
    }
}