using Application.Commands.CreatePublisher;
using Application.Commands.UpdatePublisher;

namespace Application.Services
{
    public interface IPublisherWriteService
    {
        Task AddAsync(CreatePublisherCommand request);
        Task UpdateAsync(UpdatePublisherCommand request);
        Task DeleteByIdAsync(int id);
    }
}