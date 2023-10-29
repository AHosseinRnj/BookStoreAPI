using MediatR;

namespace Application.Commands.CreateCategory
{
    public record CreateCategoryCommand(int id, string name) : IRequest<Unit>;
}