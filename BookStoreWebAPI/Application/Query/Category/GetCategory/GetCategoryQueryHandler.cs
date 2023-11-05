using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Query.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryQueryResponse>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public GetCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetCategoryQueryHandler));
        }

        public async Task<GetCategoryQueryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            GetCategoryQueryResponse category;

            try
            {
                _logger.Info("Received a request to get a Category by ID: " + request.id);
                category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(request.id);
            }
            catch (Exception ex)
            {
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