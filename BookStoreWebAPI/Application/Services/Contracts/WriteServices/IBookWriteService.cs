using Application.Commands.CreateBook;
using Application.Commands.UpdateBook;

namespace Application.Services
{
    public interface IBookWriteService
    {
        Task AddAsync(CreateBookCommand request);
        Task UpdateAsync(UpdateBookCommand request);
        Task DeleteByIdAsync(int id);
    }
}