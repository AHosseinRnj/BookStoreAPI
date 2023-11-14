using Application.Query.Author.GetAuthor;
using Application.Query.GetBook;

namespace Application.Services
{
    public interface IAuthorReadService
    {
        Task<IEnumerable<GetAuthorQueryResponse>> GetAuthorsAsync();
        Task<GetAuthorQueryResponse> GetAuthorById(int id);
        Task<IEnumerable<GetBookQueryResponse>> GetAuthorBooksAsync(int id);
    }
}