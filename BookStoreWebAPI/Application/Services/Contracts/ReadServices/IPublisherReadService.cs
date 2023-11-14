using Application.Query.GetBook;
using Application.Query.GetPublisher;

namespace Application.Services
{
    public interface IPublisherReadService
    {
        Task<IEnumerable<GetPublisherQueryResponse>> GetPublishersAsync();
        Task<GetPublisherQueryResponse> GetPublisherByIdAsync(int id);
        Task<IEnumerable<GetBookQueryResponse>> GetPublisherBooksAsync(int id);
    }
}