using Application.Query.GetBook;
using MediatR;

namespace Application.Query.GetBooks
{
    public record GetBooksQuery : IRequest<IEnumerable<GetBookQueryResponse>>;
}