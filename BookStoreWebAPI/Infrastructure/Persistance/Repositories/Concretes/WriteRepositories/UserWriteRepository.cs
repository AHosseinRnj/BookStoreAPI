using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly EFContext _context;
        public UserWriteRepository(EFContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var user = await _context.Users.Where(u => u.Id == id).FirstAsync();
            _context.Users.Remove(user);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
        }
    }
}