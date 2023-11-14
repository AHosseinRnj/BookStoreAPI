using Application.Services;
using MediatR;

namespace Application.Commands.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryWriteService _categoryRepository;
        public DeleteCategoryHandler(ICategoryWriteService categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryRepository.DeleteByIdAsync(request.Id);
            return Unit.Value;
        }
    }
}