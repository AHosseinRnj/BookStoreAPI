using Application.Commands.CreateOrderBook;

namespace Application.Services
{
    public interface IOrderItemWriteService
    {
        Task AddAsync(CreateOrderItemCommand request);
    }
}