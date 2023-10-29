using MediatR;

namespace Application.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(int id, string name) : IRequest<Unit>;
}