using Application.Commands.CreateOrder;
using MediatR;

namespace Application.Command.CreateOrder
{
    public record CreateOrderCommand(CreateOrderRequest Order) : IRequest<Unit>;
}