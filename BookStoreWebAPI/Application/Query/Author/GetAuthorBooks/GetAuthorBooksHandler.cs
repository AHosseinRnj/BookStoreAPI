using Application.Query.GetAuthorBooks;
using Application.Query.GetBook;
using Application.Repositpries;
using MediatR;

namespace Application.Query.Author.GetAuthorBooks
{
    public class GetAuthorBooksHandler : IRequestHandler<GetAuthorBooksQuery, IEnumerable<GetBookQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAuthorBooksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetAuthorBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.AuthorRepository.GetAuthorBooksAsync(request.id);
            _unitOfWork.Commit();
            return result;
        }
    }
}