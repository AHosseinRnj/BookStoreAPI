using Application.Query.GetOrderBook;
using Application.Repositpries;
using MediatR;

namespace Application.Query.GetOrderBooks
{
    public class GetOrderBooksQueryHandler : IRequestHandler<GetOrderBooksQuery, IEnumerable<GetOrderBookQueryResponse>>
    {
        private IUnitOfWork _unitOfWork;
        public GetOrderBooksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetOrderBookQueryResponse>> Handle(GetOrderBooksQuery request, CancellationToken cancellationToken)
        {
            var orderBooks = await _unitOfWork.OrderBookRepository.GetOrderBooksAsync();
            _unitOfWork.Commit();

            return orderBooks;
        }
    }
}