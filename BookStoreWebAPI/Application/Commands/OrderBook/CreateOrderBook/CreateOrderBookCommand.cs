using MediatR;

namespace Application.Commands.CreateOrderBook
{
    public record CreateOrderBookCommand(int orderId, int bookId, int quantity, double Price) : IRequest<Unit>;
}