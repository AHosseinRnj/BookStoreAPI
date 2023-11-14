using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistance.Repositories
{
    public class OrderItemWriteRepository : IOrderItemWriteRepository
    {
        private readonly EFContext _context;
        public OrderItemWriteRepository(EFContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
        }
    }
}