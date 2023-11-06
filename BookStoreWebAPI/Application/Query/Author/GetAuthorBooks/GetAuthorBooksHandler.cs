using Application.Query.GetAuthorBooks;
using Application.Query.GetBook;
using Application.Services;
using MediatR;

namespace Application.Query.Author.GetAuthorBooks
{
    public class GetAuthorBooksHandler : IRequestHandler<GetAuthorBooksQuery, IEnumerable<GetBookQueryResponse>>
    {
        private readonly IAuthorService _authorService;
        public GetAuthorBooksHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetAuthorBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await _authorService.GetAuthorBooksAsync(request.id);
            return result;
        }
    }
}