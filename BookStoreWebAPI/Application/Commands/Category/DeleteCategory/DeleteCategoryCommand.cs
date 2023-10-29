using MediatR;

namespace Application.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(int id) : IRequest<Unit>;
}