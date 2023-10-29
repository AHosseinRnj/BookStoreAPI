using MediatR;

namespace Application.Query.GetCategory
{
    public record GetCategoryQuery(int id) : IRequest<GetCategoryQueryResponse>;
}