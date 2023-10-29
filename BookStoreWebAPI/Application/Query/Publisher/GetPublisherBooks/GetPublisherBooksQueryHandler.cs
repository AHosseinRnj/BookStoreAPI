using Application.Query.GetBook;
using Application.Repositpries;
using MediatR;

namespace Application.Query.GetPublisherBooks
{
    public class GetPublisherBooksQueryHandler : IRequestHandler<GetPublisherBooksQuery, IEnumerable<GetBookQueryResponse>>
    {
        private IUnitOfWork _unitOfWork;
        public GetPublisherBooksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetPublisherBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.PublisherRepository.GetPublisherBooksAsync(request.id);
            _unitOfWork.Commit();

            return result;
        }
    }
}