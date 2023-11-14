using MediatR;

namespace Application.Query.GetUser
{
    public record GetUserQuery(int id) : IRequest<GetUserQueryResponse>;
}