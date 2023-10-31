using Application.Commands.CreateBook;
using Application.Commands.UpdateBook;
using Application.Query.GetBook;

namespace Application.Repositpries
{
    public interface IBookRepository
    {
        Task AddAsync(CreateBookCommand book);
        Task<IEnumerable<GetBookQueryResponse>> GetBooksAsync();
        Task<GetBookQueryResponse> GetBookByIdAsync(int id);
        Task UpdateAsync(UpdateBookCommand book);
        Task DeleteByIdAsync(int id);
    }
}