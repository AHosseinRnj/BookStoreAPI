using Application.Services;
using MediatR;

namespace Application.Commands.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Unit>
    {
        private readonly ICategoryService _categoryService;
        public CreateCategoryHandler(ICategoryService categoryRepository)
        {
            _categoryService = categoryRepository;
        }

        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryService.AddAsync(request);
            return Unit.Value;
        }
    }
}