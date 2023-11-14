using Application.Services;
using MediatR;

namespace Application.Commands.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Unit>
    {
        private readonly ICategoryWriteService _categoryService;
        public CreateCategoryHandler(ICategoryWriteService categoryRepository)
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