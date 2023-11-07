using MediatR;

namespace Application.Command.CreateOrder
{
    public record CreateOrderCommand(int id, DateTime orderDate, int userId) : IRequest<Unit>;
}