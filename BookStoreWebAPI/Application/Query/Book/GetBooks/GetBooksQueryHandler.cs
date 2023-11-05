using Application.Query.GetBook;
using Application.Repositpries;
using log4net;
using MediatR;
using System.Collections.Generic;

namespace Application.Query.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<GetBookQueryResponse>>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        public GetBooksQueryHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _logger = LogManager.GetLogger(typeof(GetBooksQueryHandler));
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetBookQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get all books");
                result = await _bookRepository.GetBooksAsync();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting Books: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Books retrieved successfully.");
            return result;
        }
    }
}