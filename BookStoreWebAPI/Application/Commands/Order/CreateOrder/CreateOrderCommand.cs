using MediatR;

namespace Application.Command.CreateOrder
{
    public record CreateOrderCommand(int id, double totalPrice, DateTime orderDate, int userId) : IRequest<Unit>;
}