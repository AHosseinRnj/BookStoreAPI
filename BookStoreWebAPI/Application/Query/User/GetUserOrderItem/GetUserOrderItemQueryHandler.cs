using Application.Services;
using MediatR;

namespace Application.Query.GetUserOrders
{
    public class GetUserOrderItemQueryHandler : IRequestHandler<GetUserOrderItemQuery, IEnumerable<GetUserOrderItemQueryResponse>>
    {
        private readonly IUserService _userService;
        public GetUserOrderItemQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<GetUserOrderItemQueryResponse>> Handle(GetUserOrderItemQuery request, CancellationToken cancellationToken)
        {
            var listOfOrders = await _userService.GetUserOrdersById(request.id);
            return listOfOrders;
        }
    }
}