using Application.Query.GetOrder;
using Application.Repositpries;
using MediatR;

namespace Application.Query.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<GetOrderQueryResponse>>
    {
        private IUnitOfWork _unitOfWork;
        public GetOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetOrderQueryResponse>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var listOfOrders = await _unitOfWork.OrderRepository.GetOrdersAsync();
            _unitOfWork.Commit();

            return listOfOrders;
        }
    }
}