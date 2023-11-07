using Application.Query.GetBook;
using Application.Services;
using MediatR;

namespace Application.Query.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<GetBookQueryResponse>>
    {
        private readonly IBookService _bookService;
        public GetBooksQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IEnumerable<GetBookQueryResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookService.GetBooksAsync();
            return result;
        }
    }
}