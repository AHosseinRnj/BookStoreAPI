using Application.Services;
using MediatR;

namespace Application.Commands.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryService _categoryRepository;
        public DeleteCategoryHandler(ICategoryService categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryRepository.DeleteByIdAsync(request.id);
            return Unit.Value;
        }
    }
}