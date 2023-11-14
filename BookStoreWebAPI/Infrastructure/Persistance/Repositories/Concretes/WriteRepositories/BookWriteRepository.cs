using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class BookWriteRepository : IBookWriteRepository
    {
        private readonly EFContext _context;
        public BookWriteRepository(EFContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var book = await _context.Books.Where(b => b.Id == id).FirstAsync();
            _context.Books.Remove(book);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
        }
    }
}