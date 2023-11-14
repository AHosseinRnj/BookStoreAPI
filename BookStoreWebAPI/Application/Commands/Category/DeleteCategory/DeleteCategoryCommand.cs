using MediatR;

namespace Application.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(int Id) : IRequest<Unit>;
}