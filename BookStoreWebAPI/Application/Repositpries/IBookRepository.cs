using Application.Commands.UpdateBook;
using Application.Query.GetBook;
using Application.Query.GetBooks;
using Domain.Entities;

namespace Application.Repositpries
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);
        Task<IEnumerable<GetBookQueryResponse>> GetBooksAsync();
        Task<GetBookQueryResponse> GetBookByIdAsync(int id);
        Task UpdateAsync(UpdateBookCommand book);
        Task DeleteByIdAsync(int id);
    }
}