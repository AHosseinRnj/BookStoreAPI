using Application.Repositpries;
using MediatR;

namespace Application.Commands.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        public DeleteCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.CategoryRepository.DeleteByIdAsync(request.id);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}