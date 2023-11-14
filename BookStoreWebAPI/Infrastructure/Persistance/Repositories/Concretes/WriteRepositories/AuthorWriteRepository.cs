using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class AuthorWriteRepository : IAuthorWriteRepository
    {
        private readonly EFContext _context;
        public AuthorWriteRepository(EFContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var author = await _context.Authors.Where(a => a.Id == id).FirstAsync();
            _context.Authors.Remove(author);
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
        }
    }
}