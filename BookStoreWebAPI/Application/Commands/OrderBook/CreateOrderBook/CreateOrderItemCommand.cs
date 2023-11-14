using MediatR;

namespace Application.Commands.CreateOrderBook
{
    public record CreateOrderItemCommand(int BookId, int OrderId, int Quantity, double Price) : IRequest<Unit>;
}