using Application.Services;
using log4net;
using MediatR;

namespace Application.Commands.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryWriteService _categoryRepository;
        public UpdateCategoryHandler(ICategoryWriteService categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryRepository.UpdateAsync(request);
            return Unit.Value;
        }
    }
}