using Application.Repositpries;
using log4net;
using MediatR;

namespace Application.Commands.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Unit>
    {
        private readonly ILog _logger;
        private IUnitOfWork _unitOfWork;
        public CreateCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(CreateCategoryHandler));
        }

        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("Received a request to add a Category.");
                await _unitOfWork.CategoryRepository.AddAsync(request);
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding a Category: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Category added successfully.");
            return Unit.Value;
        }
    }
}