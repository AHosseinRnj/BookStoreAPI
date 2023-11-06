using Application.Query.Author.GetAuthor;
using Application.Services;
using MediatR;

namespace Application.Query.GetAuthors
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<GetAuthorQueryResponse>>
    {
        private readonly IAuthorService _authorService;
        public GetAuthorsQueryHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IEnumerable<GetAuthorQueryResponse>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var result = await _authorService.GetAuthorsAsync();
            return result;
        }
    }
}