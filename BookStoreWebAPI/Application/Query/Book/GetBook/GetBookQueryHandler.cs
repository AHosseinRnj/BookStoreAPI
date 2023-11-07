using Application.Services;
using MediatR;

namespace Application.Query.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, GetBookQueryResponse>
    {
        private readonly IBookService _bookService;
        public GetBookQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<GetBookQueryResponse> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookService.GetBookByIdAsync(request.id);
            return result;
        }
    }
}