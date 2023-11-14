using Application.Query.GetBook;
using Application.Services;
using MediatR;

namespace Application.Query.GetCategoryBooks
{
    public class GetCategoryBooksHandler : IRequestHandler<GetCategoryBooksQuery, IEnumerable<GetBookQueryResponse>>
    {
        private readonly ICategoryReadService _categoryRepository;
        public GetCategoryBooksHandler(ICategoryReadService categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetCategoryBooksQuery request, CancellationToken cancellationToken)
        {
            var listOfBooks = await _categoryRepository.GetCategoryBooksAsync(request.id);
            return listOfBooks;
        }
    }
}