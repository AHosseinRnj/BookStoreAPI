using Application.Services;
using MediatR;

namespace Application.Query.Author.GetAuthor
{
    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, GetAuthorQueryResponse>
    {
        private readonly IAuthorService _authorService;
        public GetAuthorQueryHandler(IAuthorService authorService)
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