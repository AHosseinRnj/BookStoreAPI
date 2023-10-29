using Application.Query.GetCategory;
using MediatR;

namespace Application.Query.GetCategories
{
    public record GetCategoriesQuery : IRequest<IEnumerable<GetCategoryQueryResponse>>;
}