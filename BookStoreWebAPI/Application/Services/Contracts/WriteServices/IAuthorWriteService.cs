using Application.Commands.CreateAuthor;
using Application.Commands.UpdateAuthor;

namespace Application.Services
{
    public interface IAuthorWriteService
    {
        Task AddAsync(CreateAuthorCommand request);
        Task UpdateAsync(UpdateAuthorCommand request);
        Task DeleteByIdAsync(int id);
    }
}