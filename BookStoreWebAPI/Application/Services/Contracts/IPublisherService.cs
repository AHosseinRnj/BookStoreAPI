using Application.Commands.CreatePublisher;
using Application.Commands.UpdatePublisher;
using Application.Query.GetBook;
using Application.Query.GetPublisher;

namespace Application.Services
{
    public interface IPublisherService
    {
        Task AddAsync(CreatePublisherCommand request);
        Task<IEnumerable<GetPublisherQueryResponse>> GetPublishersAsync();
        Task<GetPublisherQueryResponse> GetPublisherByIdAsync(int id);
        Task<IEnumerable<GetBookQueryResponse>> GetPublisherBooksAsync(int id);
        Task UpdateAsync(UpdatePublisherCommand request);
        Task DeleteByIdAsync(int id);
    }
}