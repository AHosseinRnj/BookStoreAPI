using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class PublisherWriteRepository : IPublisherWriteRepository
    {
        private readonly EFContext _context;
        public PublisherWriteRepository(EFContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Publisher publisher)
        {
            await _context.Publishers.AddAsync(publisher);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var publisher = await _context.Publishers.Where(p => p.Id == id).FirstAsync();
            _context.Publishers.Remove(publisher);
        }

        public async Task UpdateAsync(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
        }
    }
}