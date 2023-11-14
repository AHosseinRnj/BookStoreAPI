using Application.Query.GetBook;

namespace Application.Services
{
    public interface IBookReadService
    {
        Task<IEnumerable<GetBookQueryResponse>> GetBooksAsync();
        Task<GetBookQueryResponse> GetBookByIdAsync(int id);
    }
}