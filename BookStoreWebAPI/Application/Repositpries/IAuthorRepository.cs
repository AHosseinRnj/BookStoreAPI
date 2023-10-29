using Application.Commands.CreateAuthor;
using Application.Commands.UpdateAuthor;
using Application.Query.Author.GetAuthor;
using Application.Query.GetBook;

namespace Application.Repositpries
{
    public interface IAuthorRepository
    {
        Task AddAsync(CreateAuthorCommand author);
        Task<IEnumerable<GetAuthorQueryResponse>> GetAuthorsAsync();
        Task<GetAuthorQueryResponse> GetAuthorById(int id);
        Task<IEnumerable<GetBookQueryResponse>> GetAuthorBooksAsync(int id);
        Task UpdateAsync(UpdateAuthorCommand author);
        Task DeleteByIdAsync(int id);
    }
}