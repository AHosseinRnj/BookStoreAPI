using Application.Query.GetBook;
using Application.Repositpries;
using MediatR;

namespace Application.Query.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<GetBookQueryResponse>>
    {
        private IUnitOfWork _unitOfWork;
        public GetBooksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.BookRepository.GetBooksAsync();
            _unitOfWork.Commit();
            return result;
        }
    }
}