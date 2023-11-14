using Application.Command.CreateOrder;

namespace Application.Services
{
    public interface IOrderWriteService
    {
        Task AddAsync(CreateOrderCommand request);
    }
}