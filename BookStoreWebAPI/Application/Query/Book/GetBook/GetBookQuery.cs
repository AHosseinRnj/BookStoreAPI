using MediatR;

namespace Application.Query.GetBook
{
    public record GetBookQuery(int id) : IRequest<GetBookQueryResponse>;
}