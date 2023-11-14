using Domain.Entities;

namespace Application.Repositories
{ 
    public interface IBookWriteRepository
    {
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteByIdAsync(int id);
    }
}