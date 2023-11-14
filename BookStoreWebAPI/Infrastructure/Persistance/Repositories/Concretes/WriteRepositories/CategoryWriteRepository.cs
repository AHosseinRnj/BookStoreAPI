using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class CategoryWriteRepository : ICategoryWriteRepository
    {
        private readonly EFContext _context;
        public CategoryWriteRepository(EFContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var category = await _context.Categories.Where(c => c.Id == id).FirstAsync();
            _context.Categories.Remove(category);
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}