using Application.Query.GetBook;
using MediatR;

namespace Application.Query.GetAuthorBooks
{
    public record GetAuthorBooksQuery(int id) : IRequest<IEnumerable<GetBookQueryResponse>>;
}