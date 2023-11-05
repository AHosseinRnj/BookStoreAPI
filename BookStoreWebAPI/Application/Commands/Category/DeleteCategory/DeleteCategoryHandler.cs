using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public DeleteCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _logger = LogManager.GetLogger(typeof(DeleteCategoryHandler));
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to delete a Category by ID: " + request.id);
                await _unitOfWork.CategoryRepository.DeleteByIdAsync(request.id);
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting a Category: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Category deleted successfully.");
            return Unit.Value;
        }
    }
}