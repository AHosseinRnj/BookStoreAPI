using Application.Commands.CreatePublisher;
using Application.Commands.UpdatePublisher;
using Application.Query.GetBook;
using Application.Query.GetPublisher;

namespace Application.Repositpries
{
    public interface IPublisherRepository
    {
        Task AddAsync(CreatePublisherCommand publisher);
        Task<IEnumerable<GetPublisherQueryResponse>> GetPublishersAsync();
        Task<GetPublisherQueryResponse> GetPublisherByIdAsync(int id);
        Task<IEnumerable<GetBookQueryResponse>> GetPublisherBooksAsync(int id);
        Task UpdateAsync(UpdatePublisherCommand publisher);
        Task DeleteByIdAsync(int id);
    }
}