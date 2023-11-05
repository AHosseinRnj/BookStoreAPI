using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public UpdateCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(UpdateCategoryHandler));
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to update a Category.");
                await _unitOfWork.CategoryRepository.UpdateAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating a Category: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Category updated successfully.");
            return Unit.Value;
        }
    }
}