using Application.Commands.CreateBook;
using Application.Commands.UpdateBook;
using Application.Query.GetBook;

namespace Application.Services
{
    public interface IBookService
    {
        Task AddAsync(CreateBookCommand request);
        Task<IEnumerable<GetBookQueryResponse>> GetBooksAsync();
        Task<GetBookQueryResponse> GetBookByIdAsync(int id);
        Task UpdateAsync(UpdateBookCommand request);
        Task DeleteByIdAsync(int id);
    }
}