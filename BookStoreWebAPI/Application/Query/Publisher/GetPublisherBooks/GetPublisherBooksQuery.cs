using Application.Query.GetBook;
using MediatR;

namespace Application.Query.GetPublisherBooks
{
    public record GetPublisherBooksQuery(int id) : IRequest<IEnumerable<GetBookQueryResponse>>;
}