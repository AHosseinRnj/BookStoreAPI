using Application.Query.GetBook;
using MediatR;

namespace Application.Query.GetCategoryBooks
{
    public record GetCategoryBooksQuery(int id) : IRequest<IEnumerable<GetBookQueryResponse>>;
}