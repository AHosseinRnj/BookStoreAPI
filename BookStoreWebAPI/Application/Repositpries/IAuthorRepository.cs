using Application.Commands.UpdateAuthor;
using Application.Query.Author.GetAuthor;
using Domain.Entities;

namespace Application.Repositpries
{
    public interface IAuthorRepository
    {
        Task AddAsync(Author author);
        Task<IEnumerable<GetAuthorQueryResponse>> GetAuthorsAsync();
        Task<GetAuthorQueryResponse> GetAuthorById(int id);
        Task UpdateAsync(UpdateAuthorCommand author);
        Task DeleteByIdAsync(int id);
    }
}