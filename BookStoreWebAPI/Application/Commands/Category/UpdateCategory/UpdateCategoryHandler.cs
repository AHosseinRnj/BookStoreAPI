using Application.Repositpries;
using MediatR;

namespace Application.Commands.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        public UpdateCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.CategoryRepository.UpdateAsync(request);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}