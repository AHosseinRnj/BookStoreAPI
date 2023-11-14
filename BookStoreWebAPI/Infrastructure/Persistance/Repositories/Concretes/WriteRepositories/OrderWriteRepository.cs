using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistance.Repositories
{
    public class OrderWriteRepository : IOrderWriteRepository
    {
        private readonly EFContext _context;
        public OrderWriteRepository(EFContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }
    }
}