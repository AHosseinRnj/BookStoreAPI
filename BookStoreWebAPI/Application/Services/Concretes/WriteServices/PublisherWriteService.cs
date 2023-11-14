using Application.Commands.CreatePublisher;
using Application.Commands.UpdatePublisher;
using Domain.Entities;
using log4net;

namespace Application.Services
{
    public class PublisherWriteService : IPublisherWriteService
    {
        private readonly ILog _logger;
        private IEFUnitOfWork _unitOfWork;
        public PublisherWriteService(IEFUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = LogManager.GetLogger(typeof(PublisherWriteService));
        }

        public async Task AddAsync(CreatePublisherCommand request)
        {
            try
            {
                var publisher = new Publisher
                {
                    Name = request.Name,
                    Description = request.Description
                };

                await _unitOfWork.PublisherRepository.AddAsync(publisher);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error adding a Publisher: " + ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                await _unitOfWork.PublisherRepository.DeleteByIdAsync(id);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting a Publisher: " + ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateAsync(UpdatePublisherCommand request)
        {
            try
            {
                var publisher = new Publisher
                {
                    Id = request.Id,
                    Name = request.Publisher.Name,
                    Description = request.Publisher.Description
                };

                await _unitOfWork.PublisherRepository.UpdateAsync(publisher);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error updating a Publisher: " + ex.Message, ex);
                throw;
            }
        }
    }
}