using MediatR;

namespace Application.Command.CreateOrder
{
    public record CreateOrderCommand(DateTime OrderDate, int UserId) : IRequest<Unit>;
}