using Application.Query.GetCategory;
using Application.Repositpries;
using MediatR;

namespace Application.Query.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<GetCategoryQueryResponse>>
    {
        private IUnitOfWork _unitOfWork;
        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetCategoryQueryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var listOfCategories = await _unitOfWork.CategoryRepository.GetCategoriesAsync();
            _unitOfWork.Commit();

            return listOfCategories;
        }
    }
}