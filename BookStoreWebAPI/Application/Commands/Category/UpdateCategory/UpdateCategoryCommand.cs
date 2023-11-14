using MediatR;

namespace Application.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(int Id, UpdateCategoryDto Category) : IRequest<Unit>;
}