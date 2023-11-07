using Application.Services;
using MediatR;

namespace Application.Query.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryQueryResponse>
    {
        private readonly ICategoryService _categoryRepository;
        public GetCategoryQueryHandler(ICategoryService categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetCategoryQueryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.id);
            return category;
        }
    }
}