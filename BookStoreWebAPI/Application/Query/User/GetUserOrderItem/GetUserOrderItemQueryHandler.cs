using Application.Services;
using MediatR;

namespace Application.Query.GetUserOrders
{
    public class GetUserOrderItemQueryHandler : IRequestHandler<GetUserOrderItemQuery, IEnumerable<GetUserOrderItemQueryResponse>>
    {
        private readonly IUserReadService _userService;
        public GetUserOrderItemQueryHandler(IUserReadService userService)
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