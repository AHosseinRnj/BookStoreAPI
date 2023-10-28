using Application.Query.Author.GetAuthor;
using MediatR;

namespace Application.Query.GetAuthors
{
    public record GetAuthorsQuery : IRequest<IEnumerable<GetAuthorQueryResponse>>;
}