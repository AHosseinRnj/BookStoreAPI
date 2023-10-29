using Application.Repositpries;
using MediatR;

namespace Application.Commands.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Unit>
    {
        private IUnitOfWork _unitOfWork;
        public CreateCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.CategoryRepository.AddAsync(request);
            _unitOfWork.Commit();

            return Unit.Value;
        }
    }
}