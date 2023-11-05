using Application.Query.GetOrderBook;
using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetOrderBooks
{
    public class GetOrderBooksQueryHandler : IRequestHandler<GetOrderBooksQuery, IEnumerable<GetOrderBookQueryResponse>>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IOrderBookRepository _orderBookRepository;
        public GetOrderBooksQueryHandler(IUnitOfWork unitOfWork, IOrderBookRepository orderBookRepository)
        {
            _unitOfWork = unitOfWork;
            _orderBookRepository = orderBookRepository;
            _logger = LogManager.GetLogger(typeof(GetOrderBooksQueryHandler));
        }

        public async Task<IEnumerable<GetOrderBookQueryResponse>> Handle(GetOrderBooksQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetOrderBookQueryResponse> orderBooks;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get all OrderBooks");
                orderBooks = await _orderBookRepository.GetOrderBooksAsync();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting OrderBooks: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("OrderBooks retrieved successfully.");
            return orderBooks;
        }
    }
}