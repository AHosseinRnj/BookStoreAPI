using MediatR;

namespace Application.Commands.CreateCategory
{
    public record CreateCategoryCommand(string Name) : IRequest<Unit>;
}