using Application.Query.GetCategory;
using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<GetCategoryQueryResponse>>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _logger = LogManager.GetLogger(typeof(GetCategoriesQueryHandler));
        }

        public async Task<IEnumerable<GetCategoryQueryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetCategoryQueryResponse> listOfCategories;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get Categories");
                listOfCategories = await _categoryRepository.GetCategoriesAsync();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting Categories: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Categories retrieved successfully.");
            return listOfCategories;
        }
    }
}