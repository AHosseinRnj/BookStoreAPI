using Application.Commands.UpdateBook;
using Application.Query.GetBook;
using Domain.Entities;

namespace Application.Repositpries
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);
        Task<GetBookQueryResponse> GetBookByIdAsync(int id);
        Task UpdateAsync(UpdateBookCommand book);
        Task DeleteByIdAsync(int id);
    }
}