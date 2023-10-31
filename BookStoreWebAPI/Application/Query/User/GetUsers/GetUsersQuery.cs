using Application.Query.GetUser;
using MediatR;

namespace Application.Query.GetUsers
{
    public record GetUsersQuery : IRequest<IEnumerable<GetUserQueryResponse>>;
}