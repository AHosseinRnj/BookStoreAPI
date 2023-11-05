using Application.Query.GetCategory;
using Application.Repositpries;
using log4net;
using MediatR;
using System.Collections.Generic;

namespace Application.Query.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<GetCategoryQueryResponse>>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(GetCategoriesQueryHandler));
        }

        public async Task<IEnumerable<GetCategoryQueryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetCategoryQueryResponse> listOfCategories;

            try
            {
                _logger.Info("Received a request to get Categories");
                listOfCategories = await _unitOfWork.CategoryRepository.GetCategoriesAsync();
            }
            catch (Exception ex)
            {
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