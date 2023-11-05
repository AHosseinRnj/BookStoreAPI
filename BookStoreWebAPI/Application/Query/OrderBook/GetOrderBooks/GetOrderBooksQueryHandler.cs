using Application.Query.GetOrderBook;
using Application.Repositpries;
using log4net;
using MediatR;
using System.Collections.Generic;

namespace Application.Query.GetOrderBooks
{
    public class GetOrderBooksQueryHandler : IRequestHandler<GetOrderBooksQuery, IEnumerable<GetOrderBookQueryResponse>>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public GetOrderBooksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetOrderBooksQueryHandler));
        }

        public async Task<IEnumerable<GetOrderBookQueryResponse>> Handle(GetOrderBooksQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetOrderBookQueryResponse> orderBooks;

            try
            {
                _logger.Info("Received a request to get all OrderBooks");
                orderBooks = await _unitOfWork.OrderBookRepository.GetOrderBooksAsync();
            }
            catch (Exception ex)
            {
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