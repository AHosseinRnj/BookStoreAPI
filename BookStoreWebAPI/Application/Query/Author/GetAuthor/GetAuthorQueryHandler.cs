using Application.Services;
using MediatR;

namespace Application.Query.Author.GetAuthor
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, GetAuthorQueryResponse>
    {
        private readonly IAuthorReadService _authorService;
        public GetAuthorQueryHandler(IAuthorReadService authorService)
        {
            _authorService = authorService;
        }

        public async Task<GetAuthorQueryResponse> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var result = await _authorService.GetAuthorById(request.id);
            return result;
        }
    }
}