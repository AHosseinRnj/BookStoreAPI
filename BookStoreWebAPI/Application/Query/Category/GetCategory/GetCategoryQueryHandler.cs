using Application.Repositpries;
using MediatR;

namespace Application.Query.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryQueryResponse>
    {
        private IUnitOfWork _unitOfWork;
        public GetCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetCategoryQueryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(request.id);
            _unitOfWork.Commit();

            return category;
        }
    }
}