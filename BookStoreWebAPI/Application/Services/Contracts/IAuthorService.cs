using Application.Commands.CreateAuthor;
using Application.Commands.UpdateAuthor;
using Application.Query.Author.GetAuthor;
using Application.Query.GetBook;

namespace Application.Services
{
    public interface IAuthorService
    {
        Task AddAsync(CreateAuthorCommand request);
        Task<IEnumerable<GetAuthorQueryResponse>> GetAuthorsAsync();
        Task<GetAuthorQueryResponse> GetAuthorById(int id);
        Task<IEnumerable<GetBookQueryResponse>> GetAuthorBooksAsync(int id);
        Task UpdateAsync(UpdateAuthorCommand request);
        Task DeleteByIdAsync(int id);
    }
}