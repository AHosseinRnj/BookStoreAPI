using Application.Query.GetBook;
using Application.Repositpries;
using MediatR;

namespace Application.Query.GetCategoryBooks
{
    public class GetCategoryBooksHandler : IRequestHandler<GetCategoryBooksQuery, IEnumerable<GetBookQueryResponse>>
    {
        private IUnitOfWork _unitOfWork;
        public GetCategoryBooksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetCategoryBooksQuery request, CancellationToken cancellationToken)
        {
            var listOfBooks = await _unitOfWork.CategoryRepository.GetCategoryBooksAsync(request.id);
            _unitOfWork.Commit();

            return listOfBooks;
        }
    }
}