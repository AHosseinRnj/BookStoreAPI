using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryQueryResponse>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryQueryHandler(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _logger = LogManager.GetLogger(typeof(GetCategoryQueryHandler));
        }

        public async Task<GetCategoryQueryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            GetCategoryQueryResponse category;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get a Category by ID: " + request.id);
                category = await _categoryRepository.GetCategoryByIdAsync(request.id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting a Category: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Category retrieved successfully.");
            return category;
        }
    }
}